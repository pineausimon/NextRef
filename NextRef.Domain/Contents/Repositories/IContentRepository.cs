using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentRepository
{
    Task<IReadOnlyList<Content>> SearchAsync(string? keyword, string? sortBy, int? limit, int? page, CancellationToken cancellationToken);
    Task<Content?> GetByIdAsync(ContentId id, CancellationToken cancellationToken);
    Task AddAsync(Content content, CancellationToken cancellationToken);
    Task UpdateAsync(Content content, CancellationToken cancellationToken);
    Task DeleteAsync(ContentId id, CancellationToken cancellationToken);
}

