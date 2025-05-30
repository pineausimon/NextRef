using MediatR;

namespace NextRef.Application.Contents.Commands.CreateContentMention;

public record CreateContentMentionCommand(
    Guid SourceContentId,
    Guid TargetContentId,
    string Context) : IRequest<Guid>;
