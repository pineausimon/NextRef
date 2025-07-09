using FluentValidation;

namespace NextRef.Application.Features.Contributors.Queries.SearchContributors;
internal class SearchContributorsQueryValidator : AbstractValidator<SearchContributorsQuery>
{
    public SearchContributorsQueryValidator()
    {
        RuleFor(q => q.Keyword)
            .NotEmpty().WithMessage("Keyword cannot be empty");
    }
}
