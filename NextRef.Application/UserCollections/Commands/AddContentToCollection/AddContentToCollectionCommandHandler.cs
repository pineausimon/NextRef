using MediatR;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.UserCollections.Commands.AddContentToCollection;
internal class AddContentToCollectionCommandHandler : IRequestHandler<AddContentToCollectionCommand, UserCollectionItemDto>
{
    private readonly IUserCollectionItemRepository _userCollectionItemRepository;
    private readonly IUserCollectionRepository _userCollectionRepository;

    public AddContentToCollectionCommandHandler(IUserCollectionItemRepository userCollectionItemRepository, IUserCollectionRepository userCollectionRepository)
    {
        _userCollectionItemRepository = userCollectionItemRepository;
        _userCollectionRepository = userCollectionRepository;
    }
    public async Task<UserCollectionItemDto> Handle(AddContentToCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = await _userCollectionRepository.GetByIdAsync(request.UserCollectionId, cancellationToken);

        if (collection == null)
            throw new KeyNotFoundException($"Collection with ID {request.UserCollectionId} not found.");

        if(collection.UserId != request.UserId)
            throw new UnauthorizedAccessException($"User {request.UserId} does not have access to collection {request.UserCollectionId}.");

        var collectionItem = UserCollectionItem.Create(request.UserCollectionId, request.ContentId, "quote");

        await _userCollectionItemRepository.AddAsync(collectionItem, cancellationToken);

        return UserCollectionItemDto.FromDomain(collectionItem);
    }
}
