using Moq;
using NextRef.Application.Contents.Models;
using NextRef.Application.Contents.Queries.SearchContents;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Tests.Handlers.Contents;
public class SearchContentsHandlerTests
{
    private readonly Mock<IContentRepository> _repositoryMock;
    private readonly SearchContentsQueryHandler _handler;

    public SearchContentsHandlerTests()
    {
        _repositoryMock = new Mock<IContentRepository>();
        _handler = new SearchContentsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedDtos_WhenContentsAreFound()
    {
        // Arrange
        var keyword = "AI";
        var sortBy = "title";
        var limit = 10;

        var contents = new List<Content>
        {
            Content.Create("AI and Society", "desc1", DateTime.UtcNow, ""),
            Content.Create("Deep Learning", "desc2", DateTime.UtcNow, "")
        };

        _repositoryMock
            .Setup(repo => repo.SearchAsync(keyword, sortBy, limit, 1))
            .ReturnsAsync(contents);

        var request = new SearchContentsQuery(keyword, sortBy, limit);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(contents.Count, result.Count);
        Assert.All(result, dto => Assert.IsType<ContentDto>(dto));

        _repositoryMock.Verify(repo => repo.SearchAsync(keyword, sortBy, limit, 1), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoContentsAreFound()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Content>());

        var request = new SearchContentsQuery("notfound", "title", 5);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_ShouldPassCorrectArgumentsToRepository()
    {
        // Arrange
        var keyword = "neural";
        var sortBy = "createdAt";
        var limit = 3;

        var request = new SearchContentsQuery(keyword, sortBy, limit);

        _repositoryMock
            .Setup(repo => repo.SearchAsync(keyword, sortBy, limit, 1))
            .ReturnsAsync(new List<Content>());

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(repo => repo.SearchAsync(keyword, sortBy, limit, 1), Times.Once);
    }
}