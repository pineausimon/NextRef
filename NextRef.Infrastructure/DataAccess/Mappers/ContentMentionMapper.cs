using NextRef.Domain.Contents.Models;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContentMentionMapper
{
    public static ContentMention ToDomain(ContentMentionEntity entity)
    {
        return ContentMention.Rehydrate(
            entity.Id, entity.SourceContentId, entity.TargetContentId, entity.Context);
    }

}