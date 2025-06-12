using MediatR;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.UserCollections.Commands.AddContentToCollection;
public record AddContentToCollectionCommand(UserId UserId, UserCollectionId UserCollectionId, ContentId ContentId) : IRequest<UserCollectionItemDto>;
