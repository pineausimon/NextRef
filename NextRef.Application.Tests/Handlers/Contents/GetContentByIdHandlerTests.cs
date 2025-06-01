using Moq;
using NextRef.Application.Contents.Models;
using NextRef.Application.Contents.Queries.GetContentById;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using FluentAssertions;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class GetContentByIdHandlerTests
{
    private readonly Mock<IContentRepository> _contentRepositoryMock;
    private readonly GetContentByIdHandler _handler;

    public GetContentByIdHandlerTests()
    {
        _contentRepositoryMock = new Mock<IContentRepository>();
        _handler = new GetContentByIdHandler(_contentRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedDto_WhenContentExists()
    {
        // Arrange
        var contentId = ContentId.New();
        var domainContent = Content.Rehydrate(contentId, "Titre", "Type", DateTime.UtcNow, "Description");

        _contentRepositoryMock
            .Setup(repo => repo.GetByIdAsync(contentId, CancellationToken.None))
            .ReturnsAsync(domainContent);

        var query = new GetContentByIdQuery(contentId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new ContentDto
        {
            Id = domainContent.Id,
            Title = domainContent.Title,
            Type = domainContent.Type,
            PublishedAt = domainContent.PublishedAt,
            Description = domainContent.Description,
        });

        _contentRepositoryMock.Verify(repo => repo.GetByIdAsync(contentId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenContentDoesNotExist()
    {
        // Arrange
        var contentId = ContentId.New();

        _contentRepositoryMock
            .Setup(repo => repo.GetByIdAsync(contentId, CancellationToken.None))
            .ReturnsAsync((Content?)null);

        var query = new GetContentByIdQuery(contentId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _contentRepositoryMock.Verify(repo => repo.GetByIdAsync(contentId, CancellationToken.None), Times.Once);
    }
}
