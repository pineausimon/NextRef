using FluentValidation;

namespace NextRef.Application.Features.Contents.Commands.UpdateContent;
internal class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Provide ContentId to update Content");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Content title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(c => c.Type)
            .NotEmpty().WithMessage("Content must have a type ('Book', 'Podcast', 'Video', etc.)");
    }
}
