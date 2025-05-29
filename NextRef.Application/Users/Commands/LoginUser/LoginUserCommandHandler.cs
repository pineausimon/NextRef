using MediatR;
using Microsoft.AspNetCore.Identity;
using NextRef.Application.Users.Services;
using NextRef.Infrastructure.Authentication;

namespace NextRef.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly ISignInService _signInService;
    private readonly UserManager<AppUser> _userManager;

    public LoginUserCommandHandler(ISignInService signInService, UserManager<AppUser> userManager)
    {
        _signInService = signInService;
        _userManager = userManager;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return null;

        var result = await _signInService.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return null;

        // Ici tu génères un JWT ou autre token d'authentification
        return "fake-jwt-token";
    }
}
