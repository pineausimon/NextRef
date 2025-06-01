using MediatR;
using NextRef.Application.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.UserCollections.Queries.GetUserCollections;
internal class GetUserCollectionsQueryHandler : IRequestHandler<GetUserCollectionsQuery, IEnumerable<UserCollectionDto>>
{
    private readonly IUserCollectionRepository _userCollectionRepository;

    public GetUserCollectionsQueryHandler(IUserCollectionRepository userCollectionRepository)
    {
        _userCollectionRepository = userCollectionRepository;
    }
    public async Task<IEnumerable<UserCollectionDto>> Handle(GetUserCollectionsQuery request, CancellationToken cancellationToken)
    {
        var collections = await _userCollectionRepository.GetByUserIdAsync(request.UserId, CancellationToken.None);

        return collections.Select(UserCollectionDto.FromDomain);
    }
}
