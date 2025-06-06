using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributorRepository
{
    Task<Contributor?> GetByIdAsync(ContributorId id, CancellationToken cancellationToken);
    Task<Contributor?> GetByFullNameAsync(string fullName, CancellationToken cancellationToken);
    Task<IReadOnlyList<Contributor>> SearchAsync(string keyword, CancellationToken cancellationToken);
    Task AddAsync(Contributor contributor, CancellationToken cancellationToken);
    Task UpdateAsync(Contributor contributor, CancellationToken cancellationToken);
    Task DeleteAsync(ContributorId id, CancellationToken cancellationToken);
}
