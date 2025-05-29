using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Domain.Contents.Repositories;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContributionRepository : IContributionRepository
{
    private readonly DapperContext _context;

    public ContributionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contribution contribution)
    {
        const string query = @"
            INSERT INTO Core.Contributions (Id, ContributorId, ContentId, Role, CreatedAt, UpdatedAt)
            VALUES (@Id, @ContributorId, @ContentId, @Role, @CreatedAt, @UpdatedAt);";

        var parameters = new
        {
            contribution.Id,
            contribution.ContributorId,
            contribution.ContentId,
            contribution.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, parameters));
    }

    public async Task UpdateAsync(Contribution contribution)
    {
        const string query = @"
            UPDATE Core.Contributions
            SET Role = @Role,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";

        var parameters = new
        {
            contribution.Role,
            UpdatedAt = DateTime.UtcNow,
            contribution.Id,
        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, parameters));
    }

    public async Task DeleteAsync(Guid id)
    {
        const string query = "DELETE FROM Core.Contributions WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Contribution?> GetByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE Id = @Id;";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContributionEntity>(
            new CommandDefinition(query, new { Id = id }));

        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Contribution>> GetByContributorIdAsync(Guid contributorId)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE ContributorId = @ContributorId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContributionEntity>(
            new CommandDefinition(query, new { contributorId }));

        return entities.Select(e => e.ToDomain());
    }

    public async Task<IEnumerable<Contribution>> GetByContentIdAsync(Guid contentId)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE ContentId = @ContentId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContributionEntity>(
            new CommandDefinition(query, new { contentId }));

        return entities.Select(e => e.ToDomain());
    }
}
