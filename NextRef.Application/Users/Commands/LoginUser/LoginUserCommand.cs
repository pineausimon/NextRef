using MediatR;

namespace NextRef.Application.Users.Commands.LoginUser;
public record LoginUserCommand(string UserName, string Password) : IRequest<string?>;
