using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Models;

public class ContentDto
{
    public ContentId Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime PublishedAt { get; set; }
}