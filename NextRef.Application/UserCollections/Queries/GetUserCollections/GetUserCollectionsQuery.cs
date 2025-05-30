using MediatR;
using NextRef.Application.UserCollections.Models;

namespace NextRef.Application.UserCollections.Queries.GetUserCollections;

public record GetUserCollectionsQuery(Guid UserId) : IRequest<IEnumerable<UserCollectionDto>>;
