using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContentRepository : BaseRepository<ContentEntity, Guid>, IContentRepository
{
    public ContentRepository(DapperContext context) : base(context) { }
    
    public async Task<Content?> GetByIdAsync(ContentId id, CancellationToken cancellationToken)
    {
        var entity = await QuerySingleOrDefaultAsync<ContentEntity>(
            "SELECT * FROM Core.Contents WHERE Id = @Id",
            new { Id = id.Value },
            cancellationToken);

        return entity == null ? null : ContentMapper.ToDomain(entity);
    }

    public async Task AddAsync(Content content, CancellationToken cancellationToken)
    {
        var sql = @"
            INSERT INTO Core.Contents (Id, Title, Type, Description, PublishedAt, CreatedAt, UpdatedAt)
            VALUES (@Id, @Title, @Type, @Description, @PublishedAt, @CreatedAt, @UpdatedAt)";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = content.Id.Value,
            content.Title,
            content.Type,
            content.Description,
            content.PublishedAt,
            CreatedAt = now,
            UpdatedAt = now
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(Content content, CancellationToken cancellationToken)
    {
        var sql = @"
            UPDATE Core.Contents SET
                Title = @Title,
                Type = @Type,
                Description = @Description,
                PublishedAt = @PublishedAt,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var parameters = new
        {
            content.Title,
            content.Type,
            content.Description,
            content.PublishedAt,
            UpdatedAt = DateTime.UtcNow,
            Id = content.Id.Value
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(ContentId id, CancellationToken cancellationToken)
    {
        var sql = "DELETE FROM Core.Contents WHERE Id = @Id";
        var parameters = new { Id = id.Value };
        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task<IReadOnlyList<Content>> SearchAsync(string? keyword, string? sortBy, int? limit, int? page, CancellationToken cancellationToken)
    {
        int pageSize = limit ?? 20;
        int offset = ((page ?? 1) - 1) * pageSize;

        string orderBy = sortBy?.ToLower() switch
        {
            "title" => "Title ASC",
            "publishedat" => "PublishedAt DESC",
            "createdat" => "CreatedAt DESC",
            _ => "CreatedAt DESC"
        };

        var parameters = new DynamicParameters();
        parameters.Add("offset", offset);
        parameters.Add("pageSize", pageSize);

        string whereClause = "";
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            whereClause = "WHERE (Title LIKE @kw OR Description LIKE @kw)";
            parameters.Add("kw", $"%{keyword}%");
        }

        var sql = $@"
        SELECT *
        FROM Core.Contents
        {whereClause}
        ORDER BY {orderBy}
        OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

        // Mapping Dapper -> ContentEntity -> Domaine
        var entities = await QueryAsync<ContentEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContentMapper.ToDomain).ToList();
    }
}