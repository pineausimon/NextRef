using MediatR;

namespace NextRef.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<string?>;
