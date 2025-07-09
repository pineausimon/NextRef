using FluentValidation;

namespace NextRef.Application.Features.Users.Queries.GetUser;
internal class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}
