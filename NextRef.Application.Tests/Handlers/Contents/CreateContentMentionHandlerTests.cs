using Moq;
using NextRef.Application.Contents.Commands.CreateContentMention;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class CreateContentMentionHandlerTests
{
    private readonly Mock<IContentMentionRepository> _contentMentionRepositoryMock;
    private readonly CreateContentMentionCommandHandler _handler;

    public CreateContentMentionHandlerTests()
    {
        _contentMentionRepositoryMock = new Mock<IContentMentionRepository>();
        _handler = new CreateContentMentionCommandHandler(_contentMentionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateContentMentionAndReturnId()
    {
        // Arrange
        var sourceId = ContentId.New();
        var targetId = ContentId.New();
        var context = "Recommendation pour compléter le sujet de ce chapitre";

        ContentMention? savedMention = null;

        _contentMentionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<ContentMention>(), CancellationToken.None))
            .Callback<ContentMention, CancellationToken>((cm, _) => savedMention = cm)
            .Returns(Task.CompletedTask);

        var command = new CreateContentMentionCommand(sourceId, targetId, context);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contentMentionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<ContentMention>(), CancellationToken.None), Times.Once);

        Assert.NotEqual(Guid.Empty, result.Value);
        Assert.NotNull(savedMention);
        Assert.Equal(result, savedMention!.Id);
        Assert.Equal(sourceId, savedMention.SourceContentId);
        Assert.Equal(targetId, savedMention.TargetContentId);
        Assert.Equal(context, savedMention.Context);
    }
}
