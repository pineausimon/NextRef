using Moq;
using NextRef.Application.Users.Commands.RegisterUser;
using NextRef.Domain.Users;
using NextRef.Application.Users.Models;
using NextRef.Application.Users.Services;
using NextRef.Domain.Core;

namespace NextRef.Application.Tests.Handlers.Users;
public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IUserAuthService> _userAuthServiceMock = new();

    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserHandlerTests()
    {
        _handler = new RegisterUserCommandHandler(_userRepositoryMock.Object, _userAuthServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldRegisterUserAndReturnToken_WhenInputIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("user1", "user1@example.com", "SecurePassword!");

        var appUserResult = new AppUserDto(Guid.NewGuid(), "user1", "user1@example.com");
        var token = "jwt.token.here";

        _userAuthServiceMock
            .Setup(x => x.CreateUserAsync(command.UserName, command.Email, command.Password))
            .ReturnsAsync(appUserResult);

        _userAuthServiceMock
            .Setup(x => x.GenerateTokenForUserAsync(appUserResult.Username))
            .ReturnsAsync(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(token, result);

        _userAuthServiceMock.Verify(x => x.AddToRoleAsync(appUserResult.Id.ToString(), UserRoles.User), Times.Once);
        _userRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(u =>
            u.Id.Value == appUserResult.Id && u.UserName == appUserResult.Username && u.Email == appUserResult.Email)), Times.Once);
    }
}