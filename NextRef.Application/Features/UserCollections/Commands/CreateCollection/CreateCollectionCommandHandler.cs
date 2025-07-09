using MediatR;
using NextRef.Application.Features.UserCollections.Models;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.Features.UserCollections.Commands.CreateCollection;
internal class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, UserCollectionDto>
{
    private readonly IUserCollectionRepository _repository;

    public CreateCollectionCommandHandler(IUserCollectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserCollectionDto> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = UserCollection.Create(request.UserId, request.Name);
        await _repository.AddAsync(collection, cancellationToken);

        return UserCollectionDto.FromDomain(collection);
    }
}
