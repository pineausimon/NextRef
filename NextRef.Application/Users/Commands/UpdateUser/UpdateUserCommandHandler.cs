using MediatR;
using NextRef.Domain.Users;
using NextRef.Application.Users.Models;

namespace NextRef.Application.Users.Commands.UpdateUser;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
            throw new NullReferenceException();

        user.Update(request.UserName, request.UserName);
        await _userRepository.UpdateAsync(user);

        return UserDtoMapper.ToDto(user);
    }
}
