using Microsoft.AspNetCore.Identity;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.Authentication;
public class AppUser : IdentityUser<Guid>
{
    public UserEntity? Profile { get; set; }
}