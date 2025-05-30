using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContentRepository : IContentRepository
{
    private readonly DapperContext _context;

    public ContentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Content?> GetByIdAsync(ContentId id)
    {
        const string query = "SELECT * FROM Core.Contents WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContentEntity>(query, new { Id = id.Value });

        if (entity == null)
            return null;

        return ContentMapper.ToDomain(entity);
    }

    public async Task AddAsync(Content content)
    {
        const string query = @"
            INSERT INTO Core.Contents (Id, Title, Type, Description, PublishedAt, CreatedAt, UpdatedAt)
            VALUES (@Id, @Title, @Type, @Description, @PublishedAt, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(query, new
        {
            Id = content.Id.Value,
            content.Title,
            content.Type,
            content.Description,
            content.PublishedAt,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        if (added == 0)
            throw new InvalidDataException();
    }

    public async Task UpdateAsync(Content content)
    {
        const string query = @"
            UPDATE Core.Contents SET
                Title = @Title,
                Type = @Type,
                Description = @Description,
                PublishedAt = @PublishedAt,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var updated = await connection.ExecuteAsync(query, new
        {
            content.Title,
            content.Type,
            content.Description,
            content.PublishedAt,
            UpdatedAt = DateTime.UtcNow,
            Id = content.Id.Value,
        });

        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(ContentId id)
    {
        const string query = "DELETE FROM Core.Contents WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id.Value });
    }
}