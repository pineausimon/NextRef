using Microsoft.AspNetCore.Identity;
using Moq;
using NextRef.Application.Users.Commands.RegisterUser;
using NextRef.Domain.Users;
using NextRef.Infrastructure.Authentication;
using FluentAssertions;

namespace NextRef.Application.Tests.Handlers.Users;
public class RegisterUserHandlerTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserHandlerTests()
    {
        _userManagerMock = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);

        _userRepositoryMock = new Mock<IUserRepository>();

        _handler = new RegisterUserCommandHandler(_userManagerMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenUserCreatedSuccessfully()
    {
        // Arrange
        var command = new RegisterUserCommand("user1", "user1@example.com", "Password123!");

        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<AppUser, string>((user, pass) =>
            {
                user.Id = Guid.NewGuid();
            });

        _userRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password), Times.Once);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(u => u.UserName == command.UserName && u.Email == command.Email)), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenUserCreationFails()
    {
        // Arrange
        var command = new RegisterUserCommand("user2", "user2@example.com", "Password123!");

        _userManagerMock
            .Setup(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();

        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<AppUser>(), command.Password), Times.Once);
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
    }
}