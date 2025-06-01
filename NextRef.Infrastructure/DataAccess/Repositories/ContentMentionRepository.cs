using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContentMentionRepository : BaseRepository<ContentMentionEntity, Guid>, IContentMentionRepository
{
    public ContentMentionRepository(DapperContext context) : base(context) { }

    public async Task AddAsync(ContentMention mention, CancellationToken cancellationToken)
    {
        var sql = @"
            INSERT INTO Core.ContentMentions (Id, SourceContentId, TargetContentId, Context, CreatedAt, UpdatedAt)
            VALUES (@Id, @SourceContentId, @TargetContentId, @Context, @CreatedAt, @UpdatedAt);";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = mention.Id.Value,
            SourceContentId = mention.SourceContentId.Value,
            TargetContentId = mention.TargetContentId.Value,
            mention.Context,
            CreatedAt = now,
            UpdatedAt = now
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(ContentMention mention, CancellationToken cancellationToken)
    {
        var sql = @"
            UPDATE Core.ContentMentions
            SET Context = @Context,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";

        var parameters = new
        {
            mention.Context,
            UpdatedAt = DateTime.UtcNow,
            Id = mention.Id.Value,
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(ContentMentionId id, CancellationToken cancellationToken)
    {
        var sql = "DELETE FROM Core.ContentMentions WHERE Id = @Id";
        var parameters = new { Id = id.Value };
        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task<IEnumerable<ContentMention>> GetByTargetContentIdAsync(ContentId contentId, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM Core.ContentMentions WHERE TargetContentId = @TargetContentId;";
        var parameters = new { TargetContentId = contentId.Value };
        var entities = await QueryAsync<ContentMentionEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContentMentionMapper.ToDomain);
    }

    public async Task<ContentMention?> GetByIdAsync(ContentMentionId id, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM Core.ContentMentions WHERE Id = @Id;";
        var parameters = new { Id = id.Value };
        var entity = await QuerySingleOrDefaultAsync<ContentMentionEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<ContentMention>> GetBySourceContentIdAsync(ContentId contentId, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM Core.ContentMentions WHERE SourceContentId = @SourceContentId;";
        var parameters = new { SourceContentId = contentId.Value };
        var entities = await QueryAsync<ContentMentionEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContentMentionMapper.ToDomain);
    }
}
