using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionItemRepository
{
    Task<UserCollectionItem?> GetByIdAsync(UserCollectionItemId id);
    Task<IEnumerable<UserCollectionItem>> GetByCollectionIdAsync(UserCollectionId collectionId);
    Task AddAsync(UserCollectionItem item);
    Task UpdateAsync(UserCollectionItem item);
    Task DeleteAsync(UserCollectionItemId id);
    Task<bool> ExistsAsync(UserCollectionId collectionId, ContentId contentId);
}
