using MediatR;
using NextRef.Application.Users.Models;

namespace NextRef.Application.Users.Queries.GetUser;
public record GetUserQuery(Guid UserId) : IRequest<UserDto>;