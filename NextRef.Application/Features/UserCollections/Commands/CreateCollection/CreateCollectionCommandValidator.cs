using FluentValidation;

namespace NextRef.Application.Features.UserCollections.Commands.CreateCollection;
internal class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
{
    public CreateCollectionCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty();
    }
}
