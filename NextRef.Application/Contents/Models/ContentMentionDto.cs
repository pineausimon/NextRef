using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Models;

public record ContentMentionDto(
    ContentMentionId Id,
    ContentId SourceContentId,
    ContentId TargetContentId,
    string Context)
{
    public static ContentMentionDto FromDomain(ContentMention mention)
        => new(mention.Id, mention.SourceContentId, mention.TargetContentId, mention.Context);
}
