using NextRef.Application.Features.Users.Models;

namespace NextRef.Application.Features.Users.Services;
public interface IUserAuthService
{
    Task<bool> CheckPasswordSignInAsync(string userName, string password, bool lockoutOnFailure);
    Task<AppUserDto> CreateUserAsync(string username, string email, string password);
    Task AddToRoleAsync(string userId, string role);
    Task<string> GenerateTokenForUserAsync(string username);
}
