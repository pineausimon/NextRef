using MediatR;

namespace NextRef.Application.UserCollections.Commands.CreateCollection;

public record CreateCollectionCommand(Guid UserId, string Name) : IRequest<Guid>;

