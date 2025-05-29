using Moq;
using NextRef.Application.Users.Commands.UpdateUser;
using NextRef.Domain.Users;

namespace NextRef.Application.Tests.Handlers.Users;
public class UpdateUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new UpdateUserCommandHandler(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsNullReferenceException()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User?)null);

        var command = new UpdateUserCommand(Guid.NewGuid(), "newUserName", "newEmail");

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserExists_UpdatesUserAndReturnsDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = User.Rehydrate(userId, "oldUserName", "oldEmail@example.com");

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId))
            .ReturnsAsync(user);

        _userRepositoryMock.Setup(r => r.UpdateAsync(user))
            .Returns(Task.CompletedTask);

        var command = new UpdateUserCommand(userId, "newUserName", "newEmail");
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.UpdateAsync(user), Times.Once);
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal("newUserName", result.UserName);
        Assert.Equal(user.Email, result.Email);
    }
}
