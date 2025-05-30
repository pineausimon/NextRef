using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserCollectionItemMapper
{
    //public static UserCollectionItemEntity FromDomain(UserCollectionItem domain)
    //{
    //    return new UserCollectionItemEntity
    //    {
    //        Id = domain.Id,
    //        CollectionId = domain.CollectionId,
    //        ContentId = domain.ContentId,
    //        Status = domain.Status.ToString(),
    //        AddedAt = domain.AddedAt,
    //        CreatedAt = DateTime.UtcNow,
    //        UpdatedAt = DateTime.UtcNow
    //    };
    //}

    public static UserCollectionItem ToDomain(this UserCollectionItemEntity entity)
    {
        // TODO : Handle UserCollectionItemStatus later, use new extensions methods to parse enum 
        //var status = Enum.TryParse<UserCollectionItemStatus>(entity.Status, out var parsed)
        //    ? parsed
        //    : UserCollectionItemStatus.Pending;

        return UserCollectionItem.Rehydrate(
            (UserCollectionItemId)entity.Id, 
            (UserCollectionId)entity.CollectionId, 
            (ContentId)entity.ContentId, 
            entity.Status, 
            entity.AddedAt);
    }
}
