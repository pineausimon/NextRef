using MediatR;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.UserCollections.Commands.CreateCollection;
internal class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, UserCollectionId>
{
    private readonly IUserCollectionRepository _repository;

    public CreateCollectionCommandHandler(IUserCollectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserCollectionId> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = UserCollection.Create(request.UserId, request.Name);
        await _repository.AddAsync(collection, cancellationToken);

        return collection.Id;
    }
}
