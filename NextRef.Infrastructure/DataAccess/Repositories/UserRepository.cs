using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserRepository : BaseRepository<UserEntity, Guid>, IUserRepository
{
    public UserRepository(DapperContext context) : base(context) { }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, UserName, Email FROM Core.Users WHERE Id = @Id";
        var parameters = new { Id = id.Value };

        var entity = await QuerySingleOrDefaultAsync<UserEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Core.Users (Id, UserName, Email, CreatedAt, UpdatedAt)
            VALUES (@Id, @UserName, @Email, @CreatedAt, @UpdatedAt)";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = user.Id.Value,
            user.UserName,
            user.Email,
            CreatedAt = now,
            UpdatedAt = now
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = "UPDATE Core.Users SET Email = @Email, UserName = @UserName, UpdatedAt = @UpdatedAt WHERE Id = @Id";


        var parameters = new
        {
            user.UserName,
            user.Email,
            UpdatedAt = DateTime.UtcNow,
            Id = user.Id.Value
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(UserId id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Core.Users WHERE Id = @Id";
        var parameters = new { Id = id.Value };
        await ExecuteAsync(sql, parameters, cancellationToken);
    }
}
