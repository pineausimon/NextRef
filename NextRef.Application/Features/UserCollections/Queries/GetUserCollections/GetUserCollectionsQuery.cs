using MediatR;
using NextRef.Application.Features.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.UserCollections.Queries.GetUserCollections;

public record GetUserCollectionsQuery(UserId UserId) : IRequest<IEnumerable<UserCollectionDto>>;
