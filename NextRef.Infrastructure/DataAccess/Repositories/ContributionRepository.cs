using Dapper;
using NextRef.Domain.Contents.Models;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
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
            Id = contribution.Id.Value,
            ContributorId = contribution.ContributorId.Value,
            ContentId = contribution.ContentId.Value,
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
            Id = contribution.Id.Value,
        };

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, parameters));
    }

    public async Task DeleteAsync(ContributionId id)
    {
        const string query = "DELETE FROM Core.Contributions WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id.Value });
    }

    public async Task<Contribution?> GetByIdAsync(ContributionId id)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE Id = @Id;";

        using var connection = _context.CreateConnection();
        var entity = await connection.QuerySingleOrDefaultAsync<ContributionEntity>(
            new CommandDefinition(query, new { Id = id.Value }));

        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Contribution>> GetByContributorIdAsync(ContributorId contributorId)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE ContributorId = @ContributorId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContributionEntity>(
            new CommandDefinition(query, new { ContributionId = contributorId.Value }));

        return entities.Select(e => e.ToDomain());
    }

    public async Task<IEnumerable<Contribution>> GetByContentIdAsync(ContentId contentId)
    {
        const string query = "SELECT * FROM Core.Contributions WHERE ContentId = @ContentId;";

        using var connection = _context.CreateConnection();
        var entities = await connection.QueryAsync<ContributionEntity>(
            new CommandDefinition(query, new { ContentId = contentId.Value }));

        return entities.Select(e => e.ToDomain());
    }
}
