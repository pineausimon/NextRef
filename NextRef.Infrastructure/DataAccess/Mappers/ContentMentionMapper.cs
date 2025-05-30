using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContentMentionMapper
{
    public static ContentMention ToDomain(ContentMentionEntity entity)
    {
        return ContentMention.Rehydrate(
            (ContentMentionId)entity.Id, (ContentId)entity.SourceContentId, (ContentId)entity.TargetContentId, entity.Context);
    }

}