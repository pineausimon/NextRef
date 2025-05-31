using FluentAssertions;
using Moq;
using NextRef.Application.Caching;
using NextRef.Application.Contents.Commands.DeleteContent;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class DeleteContentHandlerTests
{
    private readonly Mock<IContentRepository> _contentRepositoryMock;
    private readonly Mock<ICacheService> _cacheMock = new();
    private readonly DeleteContentHandler _handler;

    public DeleteContentHandlerTests()
    {
        _contentRepositoryMock = new Mock<IContentRepository>();
        _handler = new DeleteContentHandler(_contentRepositoryMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallDeleteAsyncWithCorrectId()
    {
        // Arrange
        var contentId = ContentId.New();
        var command = new DeleteContentCommand(contentId);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
        _cacheMock.Setup(c => c.RemoveByPatternAsync(It.IsAny<string>())).Returns(Task.CompletedTask);


        // Assert
        await act.Should().NotThrowAsync();
        _contentRepositoryMock.Verify(repo => repo.DeleteAsync(contentId), Times.Once);
        _cacheMock.Verify(c => c.RemoveByPatternAsync("content_search:*"), Times.Once);
    }
}