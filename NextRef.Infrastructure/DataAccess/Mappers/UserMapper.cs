using NextRef.Domain.Core.Ids;
using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserMapper
{
    //public static UserEntity FromDomain(User domain)
    //{
    //    return new UserEntity
    //    {
    //        Id = domain.Id,
    //        UserName = domain.UserName,
    //        Email = domain.Email
    //    };
    //}

    public static User ToDomain(UserEntity entity)
    {
        return User.Rehydrate(
            (UserId)entity.Id, entity.UserName, entity.UserName);
    }
}
