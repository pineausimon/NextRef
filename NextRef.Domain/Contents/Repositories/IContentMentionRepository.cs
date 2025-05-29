using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentMentionRepository
{
    Task<IEnumerable<ContentMention>> GetBySourceContentIdAsync(Guid contentId);
    Task<IEnumerable<ContentMention>> GetByTargetContentIdAsync(Guid contentId);
    Task<ContentMention?> GetByIdAsync(Guid id);
    Task AddAsync(ContentMention mention);
    Task UpdateAsync(ContentMention mention);
    Task DeleteAsync(Guid contentId);
}
