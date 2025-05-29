using NextRef.Domain.Contents.Models;

namespace NextRef.Application.Contents.Models;
public static class ContentMapper
{
    public static ContentDto ToDto(Content content)
    {
        return new ContentDto
        {
            Id = content.Id,
            Title = content.Title,
            Type = content.Type,
            Description = content.Description,
            PublishedAt = content.PublishedAt
        };
    }
}