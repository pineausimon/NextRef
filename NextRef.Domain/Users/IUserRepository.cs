using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(UserId id);
}
