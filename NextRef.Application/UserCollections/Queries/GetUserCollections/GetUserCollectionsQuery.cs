using MediatR;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.UserCollections.Queries.GetUserCollections;

public record GetUserCollectionsQuery(UserId UserId) : IRequest<IEnumerable<UserCollectionDto>>;
