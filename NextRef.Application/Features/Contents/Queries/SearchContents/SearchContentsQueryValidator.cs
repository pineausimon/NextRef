using FluentValidation;

namespace NextRef.Application.Features.Contents.Queries.SearchContents;
internal class SearchContentsQueryValidator : AbstractValidator<SearchContentsQuery>
{
    public SearchContentsQueryValidator()
    {
        RuleFor(q => q.Limit)
            .GreaterThan(0)
            .WithMessage("Limit must be greater than 0.");
    }
}
