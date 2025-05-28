using NextRef.Domain.Contents;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContentMapper
{
    public static Content ToDomain(ContentEntity entity)
    {
        return Content.Rehydrate(
            entity.Id, entity.Title, entity.Type, entity.PublishedAt, entity.Description);
    }

    public static ContentEntity ToEntity(Content domain)
    {
        return new ContentEntity
        {
            Id = domain.Id,
            Title = domain.Title,
            Description = domain.Description,
            PublishedAt = domain.PublishedAt,
            Type = domain.Type
        };
    }
}