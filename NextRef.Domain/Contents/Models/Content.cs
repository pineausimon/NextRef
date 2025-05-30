using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Models;

public class Content
{
    public ContentId Id { get; private set; }
    public string Title { get; private set; }
    public string Type { get; private set; } // TODO : Create enum ContentType
    public string? Description { get; private set; }
    public DateTime PublishedAt { get; private set; }

    private Content(ContentId id, string title, string type, DateTime publishedAt, string? description)
    {
        if (String.IsNullOrEmpty(title))
            throw new ArgumentException("A Content must have a title");

        Id = id;
        Title = title;
        Type = type;
        Description = description;
        PublishedAt = publishedAt;
    }
    public static Content Rehydrate(ContentId id, string title, string type, DateTime publishedAt, string? description)
        => new(id, title, type, publishedAt, description);


    public static Content Create(string title, string type, DateTime publishedAt, string? description)
        => new (ContentId.New(), title, type, publishedAt, description);

    public void Update(string title, string type, DateTime publishedAt, string? description)
    {
        Title = title;
        Type = type;
        Description = description;
        PublishedAt = publishedAt;
    }
}
