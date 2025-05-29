namespace NextRef.Domain.UserCollections.Models
{
    public class UserCollectionItem
    {
        public Guid Id { get; private set; }
        public Guid CollectionId { get; private set; }
        public Guid ContentId { get; private set; }
        public string Status { get; private set; }
        public DateTime AddedAt { get; private set; }

        private UserCollectionItem(Guid id, Guid collectionId, Guid contentId, string status, DateTime addedAt)
        {
            Id = id;
            CollectionId = collectionId;
            ContentId = contentId;
            Status = status;
            AddedAt = addedAt;
        }

        public static UserCollectionItem Create(Guid collectionId, Guid contentId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be empty", nameof(status));

            return new UserCollectionItem(
                Guid.NewGuid(),
                collectionId,
                contentId,
                status,
                DateTime.UtcNow);
        }

        public static UserCollectionItem Rehydrate(Guid id, Guid collectionId, Guid contentId, string status, DateTime addedAt)
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