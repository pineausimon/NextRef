using MediatR;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Commands.CreateContentMention;

public record CreateContentMentionCommand(
    ContentId SourceContentId,
    ContentId TargetContentId,
    string Context) : IRequest<ContentMentionId>;
