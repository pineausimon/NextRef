using MediatR;
using NextRef.Application.Features.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Commands.CreateContentMention;

public record CreateContentMentionCommand(
    ContentId SourceContentId,
    ContentId TargetContentId,
    string Context) : IRequest<ContentMentionDto>;
