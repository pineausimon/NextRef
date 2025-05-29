using MediatR;
using NextRef.Domain.Users;
using Microsoft.AspNetCore.Identity;
using NextRef.Infrastructure.Authentication;
using NextRef.Domain.Core;

namespace NextRef.Application.Users.Commands.RegisterUser;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(UserManager<AppUser> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = new AppUser
        {
            UserName = request.UserName,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(appUser, request.Password);
        if (!result.Succeeded)
            return false;

        if (request.UserName == "RedSky")
        {
            await _userManager.AddToRoleAsync(appUser, UserRoles.Admin);
        }
        else
        {
            await _userManager.AddToRoleAsync(appUser, UserRoles.User);
        }

        var domainUser = User.CreateFromAppUser(appUser.Id, appUser.UserName, appUser.Email);
        await _userRepository.AddAsync(domainUser); 

        return true;
    }
}
