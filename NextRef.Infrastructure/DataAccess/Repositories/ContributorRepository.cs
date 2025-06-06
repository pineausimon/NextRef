using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Configuration;
using NextRef.Infrastructure.DataAccess.Entities;
using NextRef.Infrastructure.DataAccess.Mappers;

namespace NextRef.Infrastructure.DataAccess.Repositories;
public class ContributorRepository : BaseRepository<ContributorEntity, Guid>, IContributorRepository
{
    public ContributorRepository(DapperContext context) : base(context) {}


    public async Task AddAsync(Contributor contributor, CancellationToken cancellationToken)
    {
        const string sql = @"
            INSERT INTO Core.Contributors (Id, FullName, Bio, CreatedAt, UpdatedAt)
            VALUES (@Id, @FullName, @Bio, @CreatedAt, @UpdatedAt);";

        var now = DateTime.UtcNow;
        var parameters = new
        {
            Id = contributor.Id.Value,
            contributor.FullName,
            contributor.Bio,
            CreatedAt = now,
            UpdatedAt = now
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task UpdateAsync(Contributor contributor, CancellationToken cancellationToken)
    {
        const string sql = @"
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
            Id = contributor.Id.Value,
        };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task DeleteAsync(ContributorId id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM Core.Contributors WHERE Id = @Id";
        var parameters = new { Id = id.Value };

        await ExecuteAsync(sql, parameters, cancellationToken);
    }

    public async Task<Contributor?> GetByIdAsync(ContributorId id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM Core.Contributors WHERE Id = @Id;";
        var parameters = new { Id = id.Value };

        var entity = await QuerySingleOrDefaultAsync<ContributorEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<Contributor>> SearchAsync(string keyword, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM Core.Contributors WHERE LOWER(FullName) LIKE LOWER(@Keyword);";
        var parameters = new { Keyword = $"%{keyword}%" };

        var entities = await QueryAsync<ContributorEntity>(sql, parameters, cancellationToken);
        return entities.Select(ContributorMapper.ToDomain).ToList();

    }


    public async Task<Contributor?> GetByFullNameAsync(string fullName, CancellationToken cancellationToken)
    {
        var sql = "SELECT * FROM core.Contributors WHERE FullName = @FullName";
        var parameters = new { FullName = fullName };

        var entity = await QuerySingleOrDefaultAsync<ContributorEntity>(sql, parameters, cancellationToken);
        return entity?.ToDomain();
    }
}
