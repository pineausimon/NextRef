using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionRepository
{
    Task<UserCollection?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserCollection>> GetByUserIdAsync(Guid userId);
    Task AddAsync(UserCollection collection);
    Task UpdateAsync(UserCollection collection);
    Task DeleteAsync(Guid id);
}