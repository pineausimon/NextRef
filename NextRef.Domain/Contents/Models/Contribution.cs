namespace NextRef.Domain.Contents.Models
{
    public class Contribution
    {
        public Guid Id { get; private set; }
        public Guid ContributorId { get; private set; }
        public Guid ContentId { get; private set; }
        public string Role { get; private set; }

        private Contribution(Guid id, Guid contributorId, Guid contentId, string role)
        {
            Id = id;
            ContributorId = contributorId;
            ContentId = contentId;
            Role = role;
        }

        public static Contribution Create(Guid contributorId, Guid contentId, string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            return new Contribution(
                Guid.NewGuid(),
                contributorId,
                contentId,
                role);
        }

        public static Contribution Rehydrate(Guid id, Guid contributorId, Guid contentId, string role)
        {
            return new Contribution(id, contributorId, contentId, role);
        }

        public void ChangeRole(string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole))
                throw new ArgumentException("Role cannot be empty", nameof(newRole));
            Role = newRole;
        }
    }
}
