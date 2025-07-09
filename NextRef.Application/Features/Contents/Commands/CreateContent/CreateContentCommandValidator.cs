using FluentValidation;

namespace NextRef.Application.Features.Contents.Commands.CreateContent;
public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
{
    public CreateContentCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Content title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(c => c.Type)
            .NotEmpty().WithMessage("Content must have a type ('Book', 'Podcast', 'Video', etc.)");


        RuleForEach(c => c.ExistingContributions)
            .ChildRules(existing =>
            {
                existing
                    .RuleFor(e => e.ContributorId)
                    .NotEmpty().WithMessage("Contribution on existing Contributor must provide a ContributorId");

                existing
                    .RuleFor(e => e.Role)
                    .NotEmpty().WithMessage("Contribution must specify a Role");
            });

        RuleForEach(c => c.NewContributions)
            .ChildRules(existing =>
            {
                existing
                    .RuleFor(e => e.FullName)
                    .NotEmpty().WithMessage("Contribution with new Contributor must provide its FullName");

                existing
                    .RuleFor(e => e.Role)
                    .NotEmpty().WithMessage("Contribution must specify a Role");
            });
    }
}
