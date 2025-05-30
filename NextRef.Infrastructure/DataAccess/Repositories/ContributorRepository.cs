using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContributorRepository : IContributorRepository
{
    private readonly DapperContext _context;

    public ContributorRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contributor contributor)
    {
        const string query = @"
            INSERT INTO Core.Contributors (Id, FullName, Bio, CreatedAt, UpdatedAt)
            VALUES (@Id, @FullName, @Bio, @CreatedAt, @UpdatedAt);";

        var parameters = new
        {
            contributor.Id,
            contributor.FullName,
            contributor.Bio,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, parameters));
    }

    public async Task UpdateAsync(Contributor contributor)
    {
        const string query = @"
            UPDATE Core.Contributors
            SET FullName = @FullName,
                Bio = @Bio,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";

        var parameters = new
        {
            contributor.FullName,
            contributor.Bio,
            UpdatedAt = DateTime.UtcNow,
            contributor.Id,
        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, parameters));
    }

    public async Task DeleteAsync(ContributorId id)
    {
        const string query = "DELETE FROM Core.Contributors WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Contributor?> GetByIdAsync(ContributorId id)
    {
        const string query = "SELECT * FROM Core.Contributors WHERE Id = @Id;";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContributorEntity>(
            new CommandDefinition(query, new { Id = id }));

        return entity?.ToDomain();
    }
    public async Task<Contributor?> GetByFullNameAsync(string fullName)
    {
        var sql = "SELECT * FROM core.Contributors WHERE FullName = @FullName";
        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContributorEntity>(
            sql, new { FullName = fullName });

        return entity?.ToDomain();
    }
}
