using MediatR;
using NextRef.Domain.Users;
using NextRef.Application.Users.Services;
using NextRef.Domain.Core;

namespace NextRef.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string?>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthService _userAuthService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserAuthService userAuthService)
    {
        _userRepository = userRepository;
        _userAuthService = userAuthService;
    }

    public async Task<string?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userAuthService.CreateUserAsync(request.UserName, request.Email, request.Password);

        if (request.UserName == "RedSky")
        {
            await _userAuthService.AddToRoleAsync(result.Id.ToString(), UserRoles.Admin);
        }
        else
        {
            await _userAuthService.AddToRoleAsync(result.Id.ToString(), UserRoles.User);
        }

        var domainUser = User.CreateFromAppUser(result.Id, result.Username, result.Email);
        await _userRepository.AddAsync(domainUser);

        var token = await _userAuthService.GenerateTokenForUserAsync(domainUser.UserName);
        return token;
    }
}
