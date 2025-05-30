using MediatR;
using NextRef.Application.Users.Services;

namespace NextRef.Application.Users.Commands.LoginUser;
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
            throw new UnauthorizedAccessException("Invalid credentials");

        var token = await _userAuthService.GenerateTokenForUserAsync(request.UserName);

        return token;
    }
}
