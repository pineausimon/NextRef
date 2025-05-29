using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentRepository
{
    Task<Content?> GetByIdAsync(Guid id);
    Task AddAsync(Content content);
    Task UpdateAsync(Content content);
    Task DeleteAsync(Guid id);
}

