using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserCollectionItemMapper
{
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
