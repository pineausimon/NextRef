using MediatR;
using NextRef.Application.Users.Services;

namespace NextRef.Application.Users.Commands.LoginUser;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly IUserAuthService _userAuthService;

    public LoginUserCommandHandler(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userAuthService.CheckPasswordSignInAsync(request.UserName, request.Password, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = await _userAuthService.GenerateTokenForUserAsync(request.UserName);

        return token;
    }
}
