using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NextRef.Application.Users.Commands.UpdateUser;
internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
        RuleFor(c => c.Email)
            .NotEmpty().EmailAddress();
        RuleFor(c => c.UserName)
            .NotEmpty();
    }
}
