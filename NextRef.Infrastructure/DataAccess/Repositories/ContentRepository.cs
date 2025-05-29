using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
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

    public async Task<Content?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Content WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContentEntity>(query, new { Id = id });

        if (entity == null)
            return null;

        return ContentMapper.ToDomain(entity);
    }

    public async Task AddAsync(Content content)
    {
        const string query = @"
            INSERT INTO Content (Id, Title, Type, Description, PublishedAt, CreatedAt, UpdatedAt)
            VALUES (@Id, @Title, @Type, @Description, @PublishedAt, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        var added = await connection.ExecuteAsync(query, new
        {
            content.Id,
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
            UPDATE Content SET
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
            content.Id
        });

        if (updated == 0)
            throw new InvalidDataException();
    }

    public async Task DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM Content WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}