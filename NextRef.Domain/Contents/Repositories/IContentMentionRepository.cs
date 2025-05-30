using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentMentionRepository
{
    Task<IEnumerable<ContentMention>> GetBySourceContentIdAsync(ContentId contentId);
    Task<IEnumerable<ContentMention>> GetByTargetContentIdAsync(ContentId contentId);
    Task<ContentMention?> GetByIdAsync(ContentMentionId id);
    Task AddAsync(ContentMention mention);
    Task UpdateAsync(ContentMention mention);
    Task DeleteAsync(ContentMentionId contentId);
}
