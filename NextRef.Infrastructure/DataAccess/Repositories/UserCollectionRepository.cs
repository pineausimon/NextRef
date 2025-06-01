using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserCollectionRepository : BaseRepository<UserCollectionEntity, Guid>, IUserCollectionRepository
{
    public UserCollectionRepository(DapperContext context) : base(context) { }

    public async Task<UserCollection?> GetByIdAsync(UserCollectionId id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.UserCollections WHERE Id = @Id";
        var parameters = new { Id = id.Value };
        var entity = await QuerySingleOrDefaultAsync<UserCollectionEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<UserCollection>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.UserCollections WHERE UserId = @UserId";
        var parameters = new { UserId = userId.Value };
        var entities = await QueryAsync<UserCollectionEntity>(sql, parameters, cancellationToken);
        return entities.Select(UserCollectionMapper.ToDomain);
    }

    public async Task AddAsync(UserCollection collection, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Core.UserCollections (Id, UserId, Name, CreatedAt, UpdatedAt)
            VALUES (@Id, @UserId, @Name, @CreatedAt, @UpdatedAt)";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = collection.Id.Value,
            UserId = collection.UserId.Value,
            collection.Name,
            CreatedAt = now,
            UpdatedAt = now
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(UserCollection collection, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Core.UserCollections 
            SET Name = @Name, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var parameters = new
        {
            collection.Name,
            UpdatedAt = DateTime.UtcNow,
            Id = collection.Id.Value
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(UserCollectionId id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Core.UserCollections WHERE Id = @Id";
        var parameters = new { Id = id.Value };
        await ExecuteAsync(sql, parameters, cancellationToken);
    }
}
