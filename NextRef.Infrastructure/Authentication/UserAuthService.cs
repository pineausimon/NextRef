using Microsoft.AspNetCore.Identity;
using NextRef.Application.Features.Users.Models;
using NextRef.Application.Features.Users.Services;

namespace NextRef.Infrastructure.Authentication;
public class UserAuthService : IUserAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public UserAuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<bool> CheckPasswordSignInAsync(string userName, string password, bool lockoutOnFailure)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
            return false;

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        return signInResult.Succeeded;
    }

    public async Task<AppUserDto> CreateUserAsync(string username, string email, string password)
    {
        var appUser = new AppUser
        {
            UserName = username,
            Email = email
        };

        var result = await _userManager.CreateAsync(appUser, password);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Error creating your profile");

        return new AppUserDto(appUser.Id, appUser.UserName!, appUser.Email!);
    }

    public async Task AddToRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<string> GenerateTokenForUserAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        var roles = await _userManager.GetRolesAsync(user);

        return _tokenService.GenerateToken(user.Id.ToString(), userName, roles);
    }
}
