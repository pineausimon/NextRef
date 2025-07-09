using Moq;
using NextRef.Domain.Contents.Repositories;
using NextRef.Application.Caching;
using NextRef.Domain.Contents.Models;
using NextRef.Application.Features.Contents.Models;
using NextRef.Application.Features.Contents.Queries.SearchContents;


namespace NextRef.Application.Tests.Handlers.Contents;
public class SearchContentsHandlerTests
{
    private readonly Mock<IContentRepository> _repoMock = new();
    private readonly Mock<ICacheService> _cacheMock = new();

    private SearchContentsQueryHandler CreateHandler() =>
        new(_repoMock.Object, _cacheMock.Object);

    [Fact]
    public async Task Handle_ReturnsFromCache_IfPresent()
    {
        // Arrange
        var query = new SearchContentsQuery("test","title", 10, 1);
        var cachedResult = new List<ContentDto> { new ContentDto { Title = "FromCache" } };
        _cacheMock.Setup(c => c.GetAsync<IReadOnlyList<ContentDto>>(It.IsAny<string>()))
                  .ReturnsAsync(cachedResult);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(cachedResult, result);
        _repoMock.Verify(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), CancellationToken.None), Times.Never);
        _cacheMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<IReadOnlyList<ContentDto>>(), null), Times.Never);
    }

    [Fact]
    public async Task Handle_QueriesRepositoryAndCaches_IfCacheMiss()
    {
        // Arrange
        var query = new SearchContentsQuery("test", "title", 10, 1);
        _cacheMock.Setup(c => c.GetAsync<IReadOnlyList<ContentDto>>(It.IsAny<string>()))
                  .ReturnsAsync((IReadOnlyList<ContentDto>?)null);

        var repoResult = new List<Content>
        {
            Content.Create("Title1", "Type1", System.DateTime.UtcNow, "desc")
        };
        _repoMock.Setup(r => r.SearchAsync(query.Keyword, query.SortBy, query.Limit, query.Page, CancellationToken.None))
                 .ReturnsAsync(repoResult);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Title1", result[0].Title);

        _repoMock.Verify(r => r.SearchAsync(query.Keyword, query.SortBy, query.Limit, query.Page, CancellationToken.None), Times.Once);
        _cacheMock.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<IReadOnlyList<ContentDto>>(), null), Times.Once);
    }
}