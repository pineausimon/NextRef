namespace NextRef.Domain.Contents.Models
{
    public class ContentMention
    {
        public Guid Id { get; private set; }
        public Guid SourceContentId { get; private set; }
        public Guid TargetContentId { get; private set; }
        public string Context { get; private set; }

        private ContentMention() { }

        private ContentMention(Guid id, Guid sourceContentId, Guid targetContentId, string context)
        {
            Id = id;
            SourceContentId = sourceContentId;
            TargetContentId = targetContentId;
            Context = context;
        }

        public static ContentMention Create(Guid sourceContentId, Guid targetContentId, string context)
        {
            return new ContentMention(
                Guid.NewGuid(),
                sourceContentId,
                targetContentId,
                context);
        }

        public static ContentMention Rehydrate(Guid id, Guid sourceContentId, Guid targetContentId, string context)
        {
            return new ContentMention(id, sourceContentId, targetContentId, context);
        }

        public void UpdateContext(string newContext)
        {
            Context = newContext;
        }
    }
}