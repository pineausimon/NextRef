using Microsoft.AspNetCore.Identity;
using Moq;
using NextRef.Application.Users.Commands.LoginUser;
using NextRef.Infrastructure.Authentication;
using NextRef.Application.Users.Services;

namespace NextRef.Application.Tests.Handlers.Users;
public class LoginUserHandlerTests
{
    private readonly Mock<ISignInService> _signInServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserHandlerTests()
    {
        _signInServiceMock = new Mock<ISignInService>();
        _tokenServiceMock = new Mock<ITokenService>();

        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        _handler = new LoginUserCommandHandler(_signInServiceMock.Object, _userManagerMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((AppUser?)null);

        var command = new LoginUserCommand("unknown", "pwd");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var user = new AppUser { UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByNameAsync("testuser"))
            .ReturnsAsync(user);

        _signInServiceMock.Setup(x => x.CheckPasswordSignInAsync(user, "wrongpwd", false))
            .ReturnsAsync(SignInResult.Failed);

        var command = new LoginUserCommand("testuser", "wrongpwd");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var user = new AppUser { Id = Guid.NewGuid(), UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByNameAsync("testuser"))
            .ReturnsAsync(user);

        _signInServiceMock.Setup(x => x.CheckPasswordSignInAsync(user, "correctpwd", false))
            .ReturnsAsync(SignInResult.Success);

        _userManagerMock.Setup(x => x.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "User" });

        _tokenServiceMock.Setup(x => x.GenerateToken(user.Id.ToString(), "testuser", It.IsAny<IList<string>>()))
            .Returns("fake-jwt-token");

        var command = new LoginUserCommand("testuser", "correctpwd");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("fake-jwt-token", result);
    }

}