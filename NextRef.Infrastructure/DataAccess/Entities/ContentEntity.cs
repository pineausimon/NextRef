namespace NextRef.Infrastructure.DataAccess.Entities;
public class ContentEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime PublishedAt { get; set; }
}