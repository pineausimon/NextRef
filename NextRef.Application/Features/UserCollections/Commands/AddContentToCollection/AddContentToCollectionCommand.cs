using MediatR;
using NextRef.Application.Features.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.UserCollections.Commands.AddContentToCollection;
public record AddContentToCollectionCommand(UserId UserId, UserCollectionId UserCollectionId, ContentId ContentId) : IRequest<UserCollectionItemDto>;
