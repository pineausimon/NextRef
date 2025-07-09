using FluentValidation;

namespace NextRef.Application.Features.UserCollections.Queries.GetUserCollections;
internal class GetUserCollectionsQueryValidator : AbstractValidator<GetUserCollectionsQuery>
{
    public GetUserCollectionsQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}
