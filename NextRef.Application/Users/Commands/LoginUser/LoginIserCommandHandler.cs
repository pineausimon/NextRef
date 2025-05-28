using MediatR;
using Microsoft.AspNetCore.Identity;
using NextRef.Infrastructure.Authentication;

namespace NextRef.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return null;

        // Ici tu génères un JWT ou autre token d'authentification
        return "fake-jwt-token";
    }
}
