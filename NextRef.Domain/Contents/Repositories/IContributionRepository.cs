using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributionRepository
{
    Task<Contribution?> GetByIdAsync(Guid id);
    Task<IEnumerable<Contribution>> GetByContentIdAsync(Guid contentId);
    Task AddAsync(Contribution contribution);
    Task DeleteAsync(Guid id);
}
