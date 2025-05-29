using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionItemRepository
{
    Task<UserCollectionItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserCollectionItem>> GetByCollectionIdAsync(Guid collectionId);
    Task AddAsync(UserCollectionItem item);
    Task UpdateAsync(UserCollectionItem item);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid collectionId, Guid contentId);
}
