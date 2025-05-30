using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Users.Models;
public class UserDto
{
    public UserId Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
}