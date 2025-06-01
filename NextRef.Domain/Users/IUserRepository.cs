using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(UserId id, CancellationToken cancellationToken);
}
