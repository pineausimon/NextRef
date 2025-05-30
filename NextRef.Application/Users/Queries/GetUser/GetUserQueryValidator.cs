using FluentValidation;

namespace NextRef.Application.Users.Queries.GetUser;
internal class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}
