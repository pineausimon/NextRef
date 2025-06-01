using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributionRepository
{
    Task<Contribution?> GetByIdAsync(ContributionId id, CancellationToken cancellationToken);
    Task<IEnumerable<Contribution>> GetByContentIdAsync(ContentId contentId, CancellationToken cancellationToken);
    Task<IEnumerable<Contribution>> GetByContributorIdAsync(ContributorId contributorId, CancellationToken cancellationToken);
    Task AddAsync(Contribution contribution, CancellationToken cancellationToken);
    Task UpdateAsync(Contribution contribution, CancellationToken cancellationToken);
    Task DeleteAsync(ContributionId id, CancellationToken cancellationToken);
}
