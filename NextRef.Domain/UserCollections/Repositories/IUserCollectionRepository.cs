using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionRepository
{
    Task<UserCollection?> GetByIdAsync(UserCollectionId id);
    Task<IEnumerable<UserCollection>> GetByUserIdAsync(UserId userId);
    Task AddAsync(UserCollection collection);
    Task UpdateAsync(UserCollection collection);
    Task DeleteAsync(UserCollectionId id);
}