using FluentValidation;

namespace NextRef.Application.Features.Users.Commands.RegisterUser;

internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand> 
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().EmailAddress(); ;
        RuleFor(c => c.Password)
            .NotEmpty();
        RuleFor(c => c.UserName)
            .NotEmpty();
    }
}
