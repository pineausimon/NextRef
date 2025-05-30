using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributionRepository
{
    Task<Contribution?> GetByIdAsync(ContributionId id);
    Task<IEnumerable<Contribution>> GetByContentIdAsync(ContentId contentId);
    Task<IEnumerable<Contribution>> GetByContributorIdAsync(ContributorId contributorId);
    Task AddAsync(Contribution contribution);
    Task UpdateAsync(Contribution contribution);
    Task DeleteAsync(ContributionId id);
}
