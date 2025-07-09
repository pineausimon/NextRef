using MediatR;
using NextRef.Application.Features.Users.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Users.Queries.GetUser;
public record GetUserQuery(UserId UserId) : IRequest<UserDto>;