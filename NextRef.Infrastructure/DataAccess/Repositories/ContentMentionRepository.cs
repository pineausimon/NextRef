using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContentMentionRepository : IContentMentionRepository
{
    private readonly DapperContext _context;

    public ContentMentionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ContentMention mention)
    {
        const string sql = @"
            INSERT INTO Core.ContentMentions (Id, SourceContentId, TargetContentId, Context, CreatedAt, UpdatedAt)
            VALUES (@Id, @SourceContentId, @TargetContentId, @Context, @CreatedAt, @UpdatedAt);";

        var parameters = new
        {
            Id = mention.Id.Value,
            SourceContentId = mention.SourceContentId.Value,
            TargetContentId = mention.TargetContentId.Value,
            mention.Context,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(sql, parameters));
    }

    public async Task UpdateAsync(ContentMention mention)
    {
        const string sql = @"
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

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(sql, parameters));
    }

    public async Task DeleteAsync(ContentMentionId contentId)
    {
        const string query = "DELETE FROM Core.ContentMentions WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = contentId.Value });
    }

    public async Task<IEnumerable<ContentMention>> GetByTargetContentIdAsync(ContentId contentId)
    {
        const string sql = "SELECT * FROM Core.ContentMentions WHERE TargetContentId = @TargetContentId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContentMentionEntity>(
            new CommandDefinition(sql, new { TargetContentId = contentId.Value }));

        return entities.Select(ContentMentionMapper.ToDomain);
    }

    public async Task<ContentMention?> GetByIdAsync(ContentMentionId contentId)
    {
        const string sql = "SELECT * FROM Core.ContentMentions WHERE Id = @Id;";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContentMentionEntity>(
            new CommandDefinition(sql, new { Id = contentId.Value }));

        return ContentMentionMapper.ToDomain(entity);
    }

    public async Task<IEnumerable<ContentMention>> GetBySourceContentIdAsync(ContentId contentId)
    {
        const string sql = "SELECT * FROM Core.ContentMentions WHERE SourceContentId = @SourceContentId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContentMentionEntity>(
            new CommandDefinition(sql, new { SourceContentId = contentId.Value }));

        return entities.Select(ContentMentionMapper.ToDomain);
    }
}
