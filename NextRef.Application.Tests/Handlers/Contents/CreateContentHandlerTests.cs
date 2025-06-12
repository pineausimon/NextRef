using Moq;
using NextRef.Application.Caching;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Models;
using NextRef.Application.Contents.Services;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class CreateContentHandlerTests
{
    private readonly Mock<IContentRepository> _contentRepositoryMock = new();
    private readonly Mock<IContributionService> _contributionServiceMock = new();
    private readonly Mock<ICacheService> _cacheMock = new();
    private readonly CreateContentHandler _handler;

    public CreateContentHandlerTests()
    {
        _handler = new CreateContentHandler(_contentRepositoryMock.Object, _contributionServiceMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_AddsContentAndContributions()
    {
        // Arrange
        var command = new CreateContentCommand("1984",
            "Book", 
            new DateTime(1949, 6, 8),
            "Dystopian novel.",
            new List<ContributionWithExistingContributorDto>
            {
                new(ContributorId.New(), "Author")
            },
            new List<ContributionWithNewContributorDto>
            {
                new("George Orwell", "Author")
            }
        );

        Content? createdContent = null;

        _contentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Content>(), CancellationToken.None))
            .Callback<Content, CancellationToken>((c, _) => createdContent = c)
            .Returns(Task.CompletedTask);

        _contributionServiceMock
            .Setup(s => s.AddContributionsAsync(It.IsAny<ContentId>(), command.ExistingContributions, command.NewContributions, CancellationToken.None))
            .Returns(Task.CompletedTask);
        _cacheMock.Setup(c => c.RemoveByPatternAsync(It.IsAny<string>())).Returns(Task.CompletedTask);


        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(createdContent);
        Assert.Equal(command.Title, createdContent!.Title);
        Assert.Equal(command.Type, createdContent.Type);
        Assert.Equal(command.PublishedAt, createdContent.PublishedAt);

        _contentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Content>(), CancellationToken.None), Times.Once);
        _contributionServiceMock.Verify(s => s.AddContributionsAsync(createdContent.Id, command.ExistingContributions, command.NewContributions, CancellationToken.None), Times.Once);
        _cacheMock.Verify(c => c.RemoveByPatternAsync("content_search:*"), Times.Once);
    }

    [Fact]
    public async Task Handle_NoContributions_StillAddsContent()
    {
        // Arrange
        var command = new CreateContentCommand(
            "Silent Book",
            "Book",
            DateTime.UtcNow,
            "No contributors",
            [],
            []
        );

        _contentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Content>(), CancellationToken.None))
            .Returns(Task.CompletedTask);

        _contributionServiceMock
            .Setup(s => s.AddContributionsAsync(
                It.IsAny<ContentId>(),
                new List<ContributionWithExistingContributorDto>(),
                new List<ContributionWithNewContributorDto>(), CancellationToken.None))
            .Returns(Task.CompletedTask);
        _cacheMock.Setup(c => c.RemoveByPatternAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Content>(), CancellationToken.None), Times.Once);
        _contributionServiceMock.Verify(s => s.AddContributionsAsync(
            It.IsAny<ContentId>(),
            new List<ContributionWithExistingContributorDto>(),
            new List<ContributionWithNewContributorDto>(), CancellationToken.None), Times.Once);
        _cacheMock.Verify(c => c.RemoveByPatternAsync("content_search:*"), Times.Once);
    }

}
