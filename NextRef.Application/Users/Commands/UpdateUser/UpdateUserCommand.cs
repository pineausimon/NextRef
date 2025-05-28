using MediatR;
using NextRef.Application.Users.Models;

namespace NextRef.Application.Users.Commands.UpdateUser;
public record UpdateUserCommand(Guid Id, string UserName, string Email) : IRequest<UserDto>;
