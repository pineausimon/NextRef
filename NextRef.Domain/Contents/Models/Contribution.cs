using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Models
{
    public class Contribution
    {
        public ContributionId Id { get; private set; }
        public ContributorId ContributorId { get; private set; }
        public ContentId ContentId { get; private set; }
        public string Role { get; private set; }

        private Contribution(ContributionId id, ContributorId contributorId, ContentId contentId, string role)
        {
            Id = id;
            ContributorId = contributorId;
            ContentId = contentId;
            Role = role;
        }

        public static Contribution Create(ContributorId contributorId, ContentId contentId, string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            return new Contribution(
                ContributionId.New(), 
                contributorId,
                contentId,
                role);
        }

        public static Contribution Rehydrate(ContributionId id, ContributorId contributorId, ContentId contentId, string role)
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
