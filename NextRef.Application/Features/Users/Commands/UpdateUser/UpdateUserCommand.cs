using MediatR;
using NextRef.Application.Features.Users.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Users.Commands.UpdateUser;
public record UpdateUserCommand(UserId Id, string UserName, string Email) : IRequest<UserDto>;
