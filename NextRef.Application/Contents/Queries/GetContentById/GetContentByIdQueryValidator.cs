using FluentValidation;

namespace NextRef.Application.Contents.Queries.GetContentById;
internal class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
{
    public GetContentByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("No ContentId found");
    }
}
