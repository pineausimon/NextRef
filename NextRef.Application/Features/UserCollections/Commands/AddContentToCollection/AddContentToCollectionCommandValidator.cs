using FluentValidation;

namespace NextRef.Application.Features.UserCollections.Commands.AddContentToCollection;
internal class AddContentToCollectionCommandValidator : AbstractValidator<AddContentToCollectionCommand>
{
    public AddContentToCollectionCommandValidator()
    {
        RuleFor(c => c.ContentId)
            .NotEmpty()
            .WithMessage("Content ID must not be empty.");

        RuleFor(c => c.UserCollectionId)
            .NotEmpty()
            .WithMessage("User Collection ID must not be empty.");

        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage("User ID must not be empty.");
    }
}
