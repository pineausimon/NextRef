using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserCollectionMapper
{
    //public static UserCollectionEntity FromDomain(UserCollection domain)
    //{
    //    return new UserCollectionEntity
    //    {
    //        Id = domain.Id,
    //        UserId = domain.UserId,
    //        Name = domain.Name,
    //        CreatedAt = DateTime.UtcNow,
    //        UpdatedAt = DateTime.UtcNow
    //    };
    //}

    public static UserCollection ToDomain(this UserCollectionEntity entity)
    {
        return UserCollection.Rehydrate((UserCollectionId)entity.Id, (UserId)entity.UserId, entity.Name);
    }
}
