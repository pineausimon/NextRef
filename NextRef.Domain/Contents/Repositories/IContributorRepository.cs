using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContributorRepository
{
    Task<Contributor?> GetByIdAsync(Guid id);
    Task<IEnumerable<Contributor>> GetByNameAsync(string name);
    Task AddAsync(Contributor contributor);
    Task UpdateAsync(Contributor contributor);
    Task DeleteAsync(Guid id);
}
