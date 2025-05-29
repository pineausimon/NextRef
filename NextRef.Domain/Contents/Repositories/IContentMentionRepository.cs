using NextRef.Domain.Contents.Models;

namespace NextRef.Domain.Contents.Repositories;
public interface IContentMentionRepository
{
    Task<IEnumerable<ContentMention>> GetByContributorIdAsync(Guid contributorId);
    Task<IEnumerable<ContentMention>> GetByContentIdAsync(Guid contentId);
    Task AddAsync(ContentMention mention);
    Task DeleteAsync(Guid contentId, Guid contributorId);
}
