using FluentValidation;

namespace NextRef.Application.Features.Contents.Queries.GetContentById;
internal class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
{
    public GetContentByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("No ContentId found");
    }
}
