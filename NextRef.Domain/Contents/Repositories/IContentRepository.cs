using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentRepository
{
    Task<Content?> GetByIdAsync(ContentId id);
    Task AddAsync(Content content);
    Task UpdateAsync(Content content);
    Task DeleteAsync(ContentId id);
}

