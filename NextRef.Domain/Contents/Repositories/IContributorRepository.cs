using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributorRepository
{
    Task<Contributor?> GetByIdAsync(ContributorId id);
    Task<Contributor?> GetByFullNameAsync(string fullName);
    Task AddAsync(Contributor contributor);
    Task UpdateAsync(Contributor contributor);
    Task DeleteAsync(ContributorId id);
}
