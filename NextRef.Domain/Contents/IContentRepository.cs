namespace NextRef.Domain.Contents;
public interface IContentRepository
{
    Task<Content?> GetByIdAsync(Guid id);
    Task AddAsync(Content content);
    Task UpdateAsync(Content content);
    Task DeleteAsync(Guid id);
}

