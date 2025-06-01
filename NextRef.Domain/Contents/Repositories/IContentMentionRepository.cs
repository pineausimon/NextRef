using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentMentionRepository
{
    Task<IEnumerable<ContentMention>> GetBySourceContentIdAsync(ContentId contentId, CancellationToken cancellationToken);
    Task<IEnumerable<ContentMention>> GetByTargetContentIdAsync(ContentId contentId, CancellationToken cancellationToken);
    Task<ContentMention?> GetByIdAsync(ContentMentionId id, CancellationToken cancellationToken);
    Task AddAsync(ContentMention mention, CancellationToken cancellationToken);
    Task UpdateAsync(ContentMention mention, CancellationToken cancellationToken);
    Task DeleteAsync(ContentMentionId id, CancellationToken cancellationToken);
}
