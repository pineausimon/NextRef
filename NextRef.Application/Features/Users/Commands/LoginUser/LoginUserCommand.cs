using MediatR;

namespace NextRef.Application.Features.Users.Commands.LoginUser;
public record LoginUserCommand(string UserName, string Password) : IRequest<string?>;
