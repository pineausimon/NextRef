using MediatR;
using NextRef.Application.Users.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Users.Commands.UpdateUser;
public record UpdateUserCommand(UserId Id, string UserName, string Email) : IRequest<UserDto>;
