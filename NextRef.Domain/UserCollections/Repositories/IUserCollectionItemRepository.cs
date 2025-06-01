using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionItemRepository
{
    Task<UserCollectionItem?> GetByIdAsync(UserCollectionItemId id, CancellationToken cancellationToken);
    Task<IEnumerable<UserCollectionItem>> GetByCollectionIdAsync(UserCollectionId collectionId, CancellationToken cancellationToken);
    Task AddAsync(UserCollectionItem item, CancellationToken cancellationToken);
    Task UpdateAsync(UserCollectionItem item, CancellationToken cancellationToken);
    Task DeleteAsync(UserCollectionItemId id, CancellationToken cancellationToken);
}
