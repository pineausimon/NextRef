using FluentValidation;

namespace NextRef.Application.UserCollections.Queries.GetUserCollections;
internal class GetUserCollectionsQueryValidator : AbstractValidator<GetUserCollectionsQuery>
{
    public GetUserCollectionsQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}
