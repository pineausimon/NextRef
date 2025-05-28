using NextRef.Domain.Users;

namespace NextRef.Application.Users.Models;
public static class UserDtoMapper
{
    public static UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName
        };
    }
}
