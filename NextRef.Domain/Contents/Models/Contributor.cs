using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.Contents.Models
{
    public class Contributor
    {
        public ContributorId Id { get; private set; }
        public string FullName { get; private set; }
        public string Bio { get; private set; }

        private Contributor(ContributorId id, string fullName, string bio)
        {
            Id = id;
            FullName = fullName;
            Bio = bio;
        }

        public static Contributor Create(string fullName, string bio)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName cannot be empty", nameof(fullName));

            return new Contributor(
                ContributorId.New(),
                fullName,
                bio);
        }

        public static Contributor Rehydrate(ContributorId id, string fullName, string bio)
        {
            return new Contributor(id, fullName, bio);
        }

        public void UpdateBio(string newBio)
        {
            Bio = newBio;
        }
    }
}