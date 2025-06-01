using MediatR;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.UserCollections.Commands.AddContentToCollection;
internal class AddContentToCollectionCommandHandler : IRequestHandler<AddContentToCollectionCommand, UserCollectionItemId>
{
    private readonly IUserCollectionItemRepository _userCollectionItemRepository;
    private readonly IUserCollectionRepository _userCollectionRepository;

    public AddContentToCollectionCommandHandler(IUserCollectionItemRepository userCollectionItemRepository, IUserCollectionRepository userCollectionRepository)
    {
        _userCollectionItemRepository = userCollectionItemRepository;
        _userCollectionRepository = userCollectionRepository;
    }
    public async Task<UserCollectionItemId> Handle(AddContentToCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await _userCollectionRepository.GetByIdAsync(request.UserCollectionId, cancellationToken);

        if (collection == null)
            throw new KeyNotFoundException($"Collection with ID {request.UserCollectionId} not found.");

        if(collection.UserId != request.UserId)
            throw new UnauthorizedAccessException($"User {request.UserId} does not have access to collection {request.UserCollectionId}.");

        var collectionItem = UserCollectionItem.Create(request.UserCollectionId, request.ContentId, "quote");

        await _userCollectionItemRepository.AddAsync(collectionItem, cancellationToken);

        return collectionItem.Id;
    }
}
