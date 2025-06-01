using NextRef.Domain.Contents.Models;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContributionRepository : BaseRepository<ContributionEntity, Guid>, IContributionRepository
{
    public ContributionRepository(DapperContext context) : base(context) {}

    public async Task AddAsync(Contribution contribution, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Core.Contributions (Id, ContributorId, ContentId, Role, CreatedAt, UpdatedAt)
            VALUES (@Id, @ContributorId, @ContentId, @Role, @CreatedAt, @UpdatedAt);";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = contribution.Id.Value,
            ContributorId = contribution.ContributorId.Value,
            ContentId = contribution.ContentId.Value,
            contribution.Role,
            CreatedAt = now,
            UpdatedAt = now,
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(Contribution contribution, CancellationToken cancellationToken)
    {
        const string sql = @"
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

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(ContributionId id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Core.Contributions WHERE Id = @Id";
        var parameters = new { Id = id.Value };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task<Contribution?> GetByIdAsync(ContributionId id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.Contributions WHERE Id = @Id;";
        var parameters = new { Id = id.Value };

        var entity = await QuerySingleOrDefaultAsync<ContributionEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Contribution>> GetByContributorIdAsync(ContributorId contributorId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.Contributions WHERE ContributorId = @ContributorId;";
        var parameters = new { ContributorId = contributorId.Value };

        var entities = await QueryAsync<ContributionEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContributionMapper.ToDomain);
    }

    public async Task<IEnumerable<Contribution>> GetByContentIdAsync(ContentId contentId, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.Contributions WHERE ContentId = @ContentId;";
        var parameters = new { ContentId = contentId.Value };

        var entities = await QueryAsync<ContributionEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContributionMapper.ToDomain);
    }
}
