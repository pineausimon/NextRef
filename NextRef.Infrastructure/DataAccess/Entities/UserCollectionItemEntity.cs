namespace NextRef.Infrastructure.DataAccess.Entities;
public class UserCollectionItemEntity : BaseEntity
{
    public Guid CollectionId { get; set; }
    public Guid ContentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; }
}
