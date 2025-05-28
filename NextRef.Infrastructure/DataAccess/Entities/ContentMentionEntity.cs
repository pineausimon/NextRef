namespace NextRef.Infrastructure.DataAccess.Entities;
public class ContentMentionEntity : BaseEntity
{
    public Guid SourceContentId { get; set; }
    public Guid TargetContentId { get; set; }
    public string Context { get; set; } = string.Empty;
}
