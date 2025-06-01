using NextRef.Domain.Core.Ids;
using NextRef.Domain.Users;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class UserMapper
{
    public static User ToDomain(this UserEntity entity)
    {
        return User.Rehydrate(
            (UserId)entity.Id, entity.UserName, entity.UserName);
    }
}
