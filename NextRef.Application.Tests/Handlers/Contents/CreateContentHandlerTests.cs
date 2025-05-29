using Moq;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Models;
using NextRef.Application.Contents.Services;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Tests.Handlers.Contents;
public class CreateContentHandlerTests
{
    private readonly Mock<IContentRepository> _contentRepositoryMock = new();
    private readonly Mock<IContributionService> _contributionServiceMock = new();
    private readonly CreateContentHandler _handler;

    public CreateContentHandlerTests()
    {
        _handler = new CreateContentHandler(_contentRepositoryMock.Object, _contributionServiceMock.Object);
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
                new(Guid.NewGuid(), "Author")
            },
            new List<ContributionWithNewContributorDto>
            {
                new("George Orwell", "Author")
            }
        );

        // On intercepte le contenu ajouté pour vérifier l'appel
        Content? createdContent = null;

        _contentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Content>()))
            .Callback<Content>(c => createdContent = c)
            .Returns(Task.CompletedTask);

        _contributionServiceMock
            .Setup(s => s.AddContributionsAsync(It.IsAny<Guid>(), command.ExistingContributions, command.NewContributions))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.NotNull(createdContent);
        Assert.Equal(command.Title, createdContent!.Title);
        Assert.Equal(command.Type, createdContent.Type);
        Assert.Equal(command.PublishedAt, createdContent.PublishedAt);

        _contentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Content>()), Times.Once);
        _contributionServiceMock.Verify(s => s.AddContributionsAsync(createdContent.Id, command.ExistingContributions, command.NewContributions), Times.Once);
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

        _contentRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Content>()))
            .Returns(Task.CompletedTask);

        _contributionServiceMock
            .Setup(s => s.AddContributionsAsync(It.IsAny<Guid>(), new List<ContributionWithExistingContributorDto>(), new List<ContributionWithNewContributorDto>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        _contentRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Content>()), Times.Once);
        _contributionServiceMock.Verify(s => s.AddContributionsAsync(It.IsAny<Guid>(), new List<ContributionWithExistingContributorDto>(), new List<ContributionWithNewContributorDto>()), Times.Once);
    }

}
