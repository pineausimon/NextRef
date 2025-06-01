using Moq;
using NextRef.Application.Users.Queries.GetUser;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.Users;

namespace NextRef.Application.Tests.Handlers.Users;
public class GetUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly GetUserQueryHandler _handler;

    public GetUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new GetUserQueryHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsNullReferenceException()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<UserId>(), CancellationToken.None))
            .ReturnsAsync((User?)null);

        var query = new GetUserQuery(UserId.New());

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserFound_ReturnsUserDto()
    {
        // Arrange
        var userId = UserId.New();
        var user = User.Rehydrate(userId, "testUser", "test@example.com");

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(user);

        var query = new GetUserQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("testUser", result.UserName);
        Assert.Equal("test@example.com", result.Email);
    }
}
