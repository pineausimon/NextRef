using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.UserCollections.Models
{
    public class UserCollectionItem
    {
        public UserCollectionItemId Id { get; private set; }
        public UserCollectionId CollectionId { get; private set; }
        public ContentId ContentId { get; private set; }
        public string Status { get; private set; }
        public DateTime AddedAt { get; private set; }

        private UserCollectionItem(UserCollectionItemId id, UserCollectionId collectionId, ContentId contentId, string status, DateTime addedAt)
        {
            Id = id;
            CollectionId = collectionId;
            ContentId = contentId;
            Status = status;
            AddedAt = addedAt;
        }

        public static UserCollectionItem Create(UserCollectionId collectionId, ContentId contentId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty", nameof(status));

            return new UserCollectionItem(
                UserCollectionItemId.New(),
                collectionId,
                contentId,
                status,
                DateTime.UtcNow);
        }

        public static UserCollectionItem Rehydrate(UserCollectionItemId id, UserCollectionId collectionId, ContentId contentId, string status, DateTime addedAt)
        {
            return new UserCollectionItem(id, collectionId, contentId, status, addedAt);
        }

        public void UpdateStatus(string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                throw new ArgumentException("Status cannot be empty", nameof(newStatus));
            Status = newStatus;
        }
    }
}