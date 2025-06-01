using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserCollectionMapper
{
    public static UserCollection ToDomain(this UserCollectionEntity entity)
    {
        return UserCollection.Rehydrate((UserCollectionId)entity.Id, (UserId)entity.UserId, entity.Name);
    }
}
