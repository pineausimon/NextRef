using FluentValidation;

namespace NextRef.Application.Features.Users.Commands.LoginUser;
internal class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(c => c.UserName)
            .NotEmpty();
        RuleFor(c => c.Password)
            .NotEmpty();
    }
}
