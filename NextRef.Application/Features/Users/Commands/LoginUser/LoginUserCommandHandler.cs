using MediatR;
using NextRef.Application.Features.Users.Services;

namespace NextRef.Application.Features.Users.Commands.LoginUser;
internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly IUserAuthService _userAuthService;

    public LoginUserCommandHandler(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var signInSuccess = await _userAuthService.CheckPasswordSignInAsync(request.UserName, request.Password, false);

        if (!signInSuccess)
            return null;

        var token = await _userAuthService.GenerateTokenForUserAsync(request.UserName);

        return token;
    }
}
