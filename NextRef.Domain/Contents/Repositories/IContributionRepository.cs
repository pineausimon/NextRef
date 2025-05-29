using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributionRepository
{
    Task<Contribution?> GetByIdAsync(Guid id);
    Task<IEnumerable<Contribution>> GetByContentIdAsync(Guid contentId);
    Task<IEnumerable<Contribution>> GetByContributorIdAsync(Guid contributorId);
    Task AddAsync(Contribution contribution);
    Task UpdateAsync(Contribution contribution);
    Task DeleteAsync(Guid id);
}
