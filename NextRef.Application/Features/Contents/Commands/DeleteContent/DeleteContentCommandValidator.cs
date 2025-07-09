using FluentValidation;

namespace NextRef.Application.Features.Contents.Commands.DeleteContent;
internal class DeleteContentCommandValidator : AbstractValidator<DeleteContentCommand>
{
    public DeleteContentCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Must provide ContentId to delete Content");

    }
}
