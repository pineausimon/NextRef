using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Models
{
    public class ContentMention
    {
        public ContentMentionId Id { get; private set; }
        public ContentId SourceContentId { get; private set; }
        public ContentId TargetContentId { get; private set; }
        public string Context { get; private set; }

        private ContentMention() { }

        private ContentMention(ContentMentionId id, ContentId sourceContentId, ContentId targetContentId, string context)
        {
            Id = id;
            SourceContentId = sourceContentId;
            TargetContentId = targetContentId;
            Context = context;
        }

        public static ContentMention Create(ContentId sourceContentId, ContentId targetContentId, string context)
        {
            return new ContentMention(
                ContentMentionId.New(), 
                sourceContentId,
                targetContentId,
                context);
        }

        public static ContentMention Rehydrate(ContentMentionId id, ContentId sourceContentId, ContentId targetContentId, string context)
        {
            return new ContentMention(id, sourceContentId, targetContentId, context);
        }

        public void UpdateContext(string newContext)
        {
            Context = newContext;
        }
    }
}