using FluentValidation;

namespace NextRef.Application.Features.Contents.Commands.CreateContentMention;
internal class CreateContentMentionCommandValidator : AbstractValidator<CreateContentMentionCommand>
{
    public CreateContentMentionCommandValidator()
    {
        RuleFor(cm => cm.Context)
            .NotEmpty().WithMessage("Content mention must have a context.");

        RuleFor(cm => cm.SourceContentId)
            .NotEmpty().WithMessage("Content mention must have a source.");
        RuleFor(cm => cm.TargetContentId)
            .NotEmpty().WithMessage("Content mention must have a target.");
    }
}
