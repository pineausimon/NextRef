using MediatR;
using Microsoft.AspNetCore.Identity;
using NextRef.Application.Users.Services;
using NextRef.Infrastructure.Authentication;

namespace NextRef.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly ISignInService _signInService;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;

    public LoginUserCommandHandler(ISignInService signInService, UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _signInService = signInService;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var result = await _signInService.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id.ToString(), user.UserName!, roles);

        return token;
    }
}
