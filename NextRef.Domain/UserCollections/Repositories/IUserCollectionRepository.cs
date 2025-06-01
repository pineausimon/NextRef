using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Domain.UserCollections.Repositories;
public interface IUserCollectionRepository
{
    Task<UserCollection?> GetByIdAsync(UserCollectionId id, CancellationToken cancellationToken);
    Task<IEnumerable<UserCollection>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken);
    Task AddAsync(UserCollection collection, CancellationToken cancellationToken);
    Task UpdateAsync(UserCollection collection, CancellationToken cancellationToken);
    Task DeleteAsync(UserCollectionId id, CancellationToken cancellationToken);
}