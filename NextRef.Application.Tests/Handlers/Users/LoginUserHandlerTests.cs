using Moq;
using NextRef.Application.Features.Users.Commands.LoginUser;
using NextRef.Application.Features.Users.Services;

namespace NextRef.Application.Tests.Handlers.Users;
public class LoginUserHandlerTests
{
    private readonly Mock<IUserAuthService> _userAuthServiceMock;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserHandlerTests()
    {
        _userAuthServiceMock = new Mock<IUserAuthService>();

        _handler = new LoginUserCommandHandler(_userAuthServiceMock.Object);
    }


    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginSucceeds()
    {
        // Arrange
        var command = new LoginUserCommand("user1","pass1");
        var fakeToken = "token123";

        _userAuthServiceMock
            .Setup(s => s.CheckPasswordSignInAsync(command.UserName, command.Password, false))
            .ReturnsAsync(true);

        _userAuthServiceMock
            .Setup(s => s.GenerateTokenForUserAsync(command.UserName))
            .ReturnsAsync(fakeToken);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(fakeToken, result);
        _userAuthServiceMock.Verify(s => s.CheckPasswordSignInAsync(command.UserName, command.Password, false), Times.Once);
        _userAuthServiceMock.Verify(s => s.GenerateTokenForUserAsync(command.UserName), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNullToken_WhenLoginFails()
    {
        // Arrange
        var command = new LoginUserCommand("user1", "wrongpass");

        _userAuthServiceMock
            .Setup(s => s.CheckPasswordSignInAsync(command.UserName, command.Password, false))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);

        _userAuthServiceMock.Verify(s => s.CheckPasswordSignInAsync(command.UserName, command.Password, false), Times.Once);
        _userAuthServiceMock.Verify(s => s.GenerateTokenForUserAsync(It.IsAny<string>()), Times.Never);
    }
}