namespace NextRef.Application.Contents.Models;

public class ContentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime PublishedAt { get; set; }
}