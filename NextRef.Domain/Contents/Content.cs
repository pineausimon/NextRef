namespace NextRef.Domain.Contents;

public class Content
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Type { get; private set; } // TODO : Create enum ContentType
    public string? Description { get; private set; }
    public DateTime PublishedAt { get; private set; }

    private Content(Guid id, string title, string type, DateTime publishedAt, string? description)
    {
        Id = id;
        Title = title;
        Type = type;
        Description = description;
        PublishedAt = publishedAt;
    }
    public static Content Rehydrate(Guid id, string title, string type, DateTime publishedAt, string? description)
        => new(id, title, type, publishedAt, description);


    public static Content Create(string title, string type, DateTime publishedAt, string? description)
        => new (Guid.NewGuid(), title, type, publishedAt, description);

    public void Update(string title, string type, DateTime publishedAt, string? description)
    {
        Title = title;
        Type = type;
        Description = description;
        PublishedAt = publishedAt;
    }
}
