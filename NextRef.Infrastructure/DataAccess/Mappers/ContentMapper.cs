using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContentMapper
{
    //public static ContentEntity FromDomain(Content domain)
    //{
    //    return new ContentEntity
    //    {
    //        Id = domain.Id,
    //        Title = domain.Title,
    //        Description = domain.Description,
    //        PublishedAt = domain.PublishedAt,
    //        Type = domain.Type
    //    };
    //}

    public static Content ToDomain(ContentEntity entity)
    {
        return Content.Rehydrate(
            (ContentId)entity.Id, entity.Title, entity.Type, entity.PublishedAt, entity.Description);
    }

}