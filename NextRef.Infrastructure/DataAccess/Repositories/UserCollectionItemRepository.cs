using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class UserCollectionItemRepository : BaseRepository<UserCollectionItemEntity, Guid>, IUserCollectionItemRepository
{
    public UserCollectionItemRepository(DapperContext context) : base(context) {}

    public async Task<UserCollectionItem?> GetByIdAsync(UserCollectionItemId id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.UserCollectionItems WHERE Id = @Id";
        var parameters = new { Id = id.Value };

        var entity = await QuerySingleOrDefaultAsync<UserCollectionItemEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<UserCollectionItem>> GetByCollectionIdAsync(UserCollectionId collectionId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.UserCollectionItems WHERE CollectionId = @CollectionId";
        var parameters = new { CollectionId = collectionId.Value };

        var entities = await QueryAsync<UserCollectionItemEntity>(sql, parameters, cancellationToken);
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(UserCollectionItem item, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Core.UserCollectionItems (Id, CollectionId, ContentId, Status, AddedAt)
            VALUES (@Id, @CollectionId, @ContentId, @Status, @AddedAt)";

        var parameters = new
        {
            Id = item.Id.Value,
            CollectionId = item.CollectionId.Value,
            ContentId = item.ContentId.Value,
            item.Status,
            item.AddedAt
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(UserCollectionItem item, CancellationToken cancellationToken)
    {
        const string sql = @"
            UPDATE Core.UserCollectionItems
            SET Status = @Status
            WHERE Id = @Id";

        var parameters = new
        {
            item.Status,
            Id = item.Id.Value
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(UserCollectionItemId id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Core.UserCollectionItems WHERE Id = @Id";
        var parameters = new { Id = id.Value };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }
}
