using Moq;
using NextRef.Application.UserCollections.Commands.AddContentToCollection;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.Tests.Handlers.UserCollections;
public class AddContentToCollectionCommandHandlerTests
{
    private readonly Mock<IUserCollectionRepository> _collectionRepoMock;
    private readonly Mock<IUserCollectionItemRepository> _itemRepoMock;
    private readonly AddContentToCollectionCommandHandler _handler;

    public AddContentToCollectionCommandHandlerTests()
    {
        _collectionRepoMock = new Mock<IUserCollectionRepository>();
        _itemRepoMock = new Mock<IUserCollectionItemRepository>();

        _handler = new AddContentToCollectionCommandHandler(
            _itemRepoMock.Object,
            _collectionRepoMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenCollectionNotFound()
    {
        // Arrange
        var command = new AddContentToCollectionCommand(
            new UserId(Guid.NewGuid()),
            new UserCollectionId(Guid.NewGuid()),
            new ContentId(Guid.NewGuid())
        );

        _collectionRepoMock.Setup(repo => repo.GetByIdAsync(command.UserCollectionId, CancellationToken.None))
            .ReturnsAsync((UserCollection)null!);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenUserIsNotOwner()
    {
        // Arrange
        var command = new AddContentToCollectionCommand(
            new UserId(Guid.NewGuid()),
            new UserCollectionId(Guid.NewGuid()),
            new ContentId(Guid.NewGuid())
        );

        var otherUserId = new UserId(Guid.NewGuid());

        _collectionRepoMock.Setup(repo => repo.GetByIdAsync(command.UserCollectionId, CancellationToken.None))
            .ReturnsAsync(UserCollection.Rehydrate(command.UserCollectionId, otherUserId, "Test Collection"));

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldAddItemAndReturnId_WhenValid()
    {
        // Arrange
        var userId = new UserId(Guid.NewGuid());
        var collectionId = new UserCollectionId(Guid.NewGuid());
        var contentId = new ContentId(Guid.NewGuid());

        var command = new AddContentToCollectionCommand(userId, collectionId, contentId);

        var collection = UserCollection.Rehydrate(collectionId, userId, "My Collection");

        _collectionRepoMock.Setup(repo => repo.GetByIdAsync(collectionId, CancellationToken.None))
            .ReturnsAsync(collection);

        UserCollectionItem? addedItem = null;

        _itemRepoMock.Setup(repo => repo.AddAsync(It.IsAny<UserCollectionItem>(), CancellationToken.None))
            .Callback<UserCollectionItem, CancellationToken>((item, _) => addedItem = item)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(addedItem);
        Assert.Equal(collectionId, addedItem!.CollectionId);
        Assert.Equal(contentId, addedItem.ContentId);
        Assert.Equal(result, UserCollectionItemDto.FromDomain(addedItem));
    }
}
