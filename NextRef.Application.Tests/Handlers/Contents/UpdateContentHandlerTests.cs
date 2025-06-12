using Moq;
using NextRef.Application.Contents.Commands.UpdateContent;
using NextRef.Domain.Contents.Repositories;
using NextRef.Application.Caching;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class UpdateContentHandlerTests
{
    private readonly Mock<IContentRepository> _repoMock = new();
    private readonly Mock<ICacheService> _cacheMock = new();

    private UpdateContentHandler CreateHandler() =>
        new(_repoMock.Object, _cacheMock.Object);

    [Fact]
    public async Task Handle_ShouldUpdateContent_AndInvalidateCache_WhenContentExists()
    {
        // Arrange
        var contentId = ContentId.New();
        var content = Content.Create("OldTitle", "Type", DateTime.UtcNow, "desc");
        _repoMock.Setup(r => r.GetByIdAsync(contentId, CancellationToken.None)).ReturnsAsync(content);
        _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Content>(), CancellationToken.None)).Returns(Task.CompletedTask);
        _cacheMock.Setup(c => c.RemoveByPatternAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        var handler = CreateHandler();
        var command = new UpdateContentCommand(
            contentId,
            "NewTitle",
            "Type",
            DateTime.UtcNow,
            "desc"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.GetByIdAsync(contentId, CancellationToken.None), Times.Once);
        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Content>(), CancellationToken.None), Times.Once);
        _cacheMock.Verify(c => c.RemoveByPatternAsync("content_search:*"), Times.Once);
        Assert.Equal("NewTitle", result.Title);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenContentNotFound()
    {
        // Arrange
        var contentId = ContentId.New();
        _repoMock.Setup(r => r.GetByIdAsync(contentId, CancellationToken.None)).ReturnsAsync((Content?)null);

        var handler = CreateHandler();
        var command = new UpdateContentCommand(
            contentId,
            "Title",
            "Type",
            DateTime.UtcNow,
            "desc"
        );

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Content>(), CancellationToken.None), Times.Never);
        _cacheMock.Verify(c => c.RemoveByPatternAsync(It.IsAny<string>()), Times.Never);
    }
}
