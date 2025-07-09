using MediatR;
using NextRef.Application.Features.UserCollections.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.UserCollections.Commands.CreateCollection;

public record CreateCollectionCommand(UserId UserId, string Name) : IRequest<UserCollectionDto>;

