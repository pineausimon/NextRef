using MediatR;
using NextRef.Domain.Users;
using NextRef.Application.Users.Models;

namespace NextRef.Application.Users.Queries.GetUser;
internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new NullReferenceException();

        return UserDtoMapper.ToDto(user);
    }
}