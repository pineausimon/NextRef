using MediatR;
using NextRef.Application.Users.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Users.Queries.GetUser;
public record GetUserQuery(UserId UserId) : IRequest<UserDto>;