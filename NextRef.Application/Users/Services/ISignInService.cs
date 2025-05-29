using Microsoft.AspNetCore.Identity;
using NextRef.Infrastructure.Authentication;

namespace NextRef.Application.Users.Services;
public interface ISignInService
{
    Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure);
}

public class SignInService : ISignInService
{
    private readonly SignInManager<AppUser> _signInManager;

    public SignInService(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password, bool lockoutOnFailure)
    {
        return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }
}
