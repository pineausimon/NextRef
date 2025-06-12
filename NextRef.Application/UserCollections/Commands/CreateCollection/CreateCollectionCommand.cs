using MediatR;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.UserCollections.Commands.CreateCollection;

public record CreateCollectionCommand(UserId UserId, string Name) : IRequest<UserCollectionDto>;

