using MediatR;
using NextRef.Domain.Users;
using NextRef.Application.Features.Users.Models;

namespace NextRef.Application.Features.Users.Commands.UpdateUser;
internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new NullReferenceException();

        user.Update(request.UserName, request.UserName);
        await _userRepository.UpdateAsync(user, CancellationToken.None);

        return UserDtoMapper.ToDto(user);
    }
}
