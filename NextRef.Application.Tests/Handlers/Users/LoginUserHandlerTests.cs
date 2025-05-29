using Microsoft.AspNetCore.Identity;
using Moq;
using NextRef.Application.Users.Commands.LoginUser;
using NextRef.Infrastructure.Authentication;
using NextRef.Application.Users.Services;

namespace NextRef.Application.Tests.Handlers.Users;
public class LoginUserHandlerTests
{
    private readonly Mock<ISignInService> _signInServiceMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserHandlerTests()
    {
        _signInServiceMock = new Mock<ISignInService>();

        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        _handler = new LoginUserCommandHandler(_signInServiceMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsNull()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUser?)null);

        var command = new LoginUserCommand("unknown", "pwd");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var user = new AppUser { UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByNameAsync("testuser"))
            .ReturnsAsync(user);

        _signInServiceMock.Setup(x => x.CheckPasswordSignInAsync(user, "wrongpwd", false))
            .ReturnsAsync(SignInResult.Failed);

        var command = new LoginUserCommand("testuser", "wrongpwd");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var user = new AppUser { UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByNameAsync("testuser"))
            .ReturnsAsync(user);

        _signInServiceMock.Setup(x => x.CheckPasswordSignInAsync(user, "correctpwd", false))
            .ReturnsAsync(SignInResult.Success);

        var command = new LoginUserCommand("testuser", "correctpwd");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("fake-jwt-token", result);
    }
}