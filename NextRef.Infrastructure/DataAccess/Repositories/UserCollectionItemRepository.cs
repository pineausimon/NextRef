using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using Dapper;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserCollectionItemRepository : IUserCollectionItemRepository
{
    private readonly DapperContext _context;

    public UserCollectionItemRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<UserCollectionItem?> GetByIdAsync(UserCollectionItemId id)
    {
        const string query = "SELECT * FROM UserCollectionItems WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<UserCollectionItemEntity>(query, new { Id = id.Value });
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<UserCollectionItem>> GetByCollectionIdAsync(UserCollectionId collectionId)
    {
        const string query = "SELECT * FROM UserCollectionItems WHERE CollectionId = @CollectionId";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<UserCollectionItemEntity>(query, new { CollectionId = collectionId.Value });
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(UserCollectionItem item)
    {const string query = @"
            INSERT INTO UserCollectionItems (Id, CollectionId, ContentId, Status, AddedAt, CreatedAt, UpdatedAt)
            VALUES (@Id, @CollectionId, @ContentId, @Status, @AddedAt, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(query, new
        {
            Id = item.Id.Value,
            CollectionId = item.CollectionId.Value,
            ContentId = item.ContentId.Value,
            item.Status,
            item.AddedAt,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        if (added == 0)
            throw new InvalidDataException();
    }

    public async Task UpdateAsync(UserCollectionItem item)
    {
        const string query = @"
            UPDATE UserCollectionItems
            SET Status = @Status, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var updated = await connection.ExecuteAsync(query, new
        {
            item.Status,
            UpdatedAt = DateTime.UtcNow,
            Id = item.Id.Value
        });


        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(UserCollectionItemId id)
    {
        const string query = "DELETE FROM UserCollectionItems WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id.Value });
    }

    public async Task<bool> ExistsAsync(UserCollectionId collectionId, ContentId contentId)
    {
        const string query = @"
            SELECT COUNT(1)
            FROM UserCollectionItems
            WHERE CollectionId = @CollectionId AND ContentId = @ContentId";

        using var connection = _context.CreateConnection();
        var exists = await connection.ExecuteScalarAsync<bool>(query, new { CollectionId = collectionId.Value, ContentId = contentId.Value });
        return exists;
    }
}
