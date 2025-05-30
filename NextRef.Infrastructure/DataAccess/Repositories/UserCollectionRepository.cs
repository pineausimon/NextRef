using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;
using NextRef.Infrastructure.DataAccess.Configuration;
using Dapper;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserCollectionRepository : IUserCollectionRepository
{
    private readonly DapperContext _context;

    public UserCollectionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<UserCollection?> GetByIdAsync(UserCollectionId id)
    {
        const string query = "SELECT * FROM Core.UserCollections WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<UserCollectionEntity>(query, new { Id = id });
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<UserCollection>> GetByUserIdAsync(UserId userId)
    {
        const string query = "SELECT * FROM Core.UserCollections WHERE UserId = @UserId";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<UserCollectionEntity>(query, new { UserId = userId });
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(UserCollection collection)
    {
        const string query = @"
            INSERT INTO Core.UserCollections (Id, UserId, Name, CreatedAt, UpdatedAt)
            VALUES (@Id, @UserId, @Name, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(query, new
        {
            collection.Id,
            collection.UserId,
            collection.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        if (added == 0)
            throw new InvalidDataException();
    }

    public async Task UpdateAsync(UserCollection collection)
    {
        const string query = @"
            UPDATE Core.UserCollections 
            SET Name = @Name, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var updated = await connection.ExecuteAsync(query, new
        {
            collection.Name,
            UpdatedAt = DateTime.UtcNow,
            collection.Id
        });


        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(UserCollectionId id)
    {
        const string query = "DELETE FROM Core.UserCollections WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}
