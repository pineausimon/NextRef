using Moq;
using NextRef.Application.Features.UserCollections.Queries.GetUserCollections;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.Tests.Handlers.UserCollections;
public class GetUserCollectionsHandlerTests
{
    private readonly Mock<IUserCollectionRepository> _userCollectionRepositoryMock;
    private readonly GetUserCollectionsQueryHandler _handler;

    public GetUserCollectionsHandlerTests()
    {
        _userCollectionRepositoryMock = new Mock<IUserCollectionRepository>();
        _handler = new GetUserCollectionsQueryHandler(_userCollectionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserCollections_WhenCollectionsExist()
    {
        // Arrange
        var userId = UserId.New();
        var collections = new List<UserCollection>
        {
            UserCollection.Rehydrate(UserCollectionId.New(), userId, "Favorites"),
            UserCollection.Rehydrate(UserCollectionId.New(), userId, "Sci-fi")
        };

        _userCollectionRepositoryMock
            .Setup(repo => repo.GetByUserIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(collections);

        var query = new GetUserCollectionsQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        var list = result.ToList();
        Assert.Equal(2, list.Count);
        Assert.Contains(list, c => c.Name == "Favorites");
        Assert.Contains(list, c => c.Name == "Sci-fi");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCollectionsFound()
    {
        // Arrange
        var userId = UserId.New();

        _userCollectionRepositoryMock
            .Setup(repo => repo.GetByUserIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(new List<UserCollection>());

        var query = new GetUserCollectionsQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
