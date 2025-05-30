using NextRef.Domain.Core.Ids;

namespace NextRef.Domain.UserCollections.Models;

public class UserCollection
{
    public UserCollectionId Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Name { get; private set; }

    private UserCollection(UserCollectionId id, UserId userId, string name)
    {
        Id = id;
        UserId = userId;
        Name = name;
    }

    public static UserCollection Create(UserId userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        return new UserCollection(
            UserCollectionId.New(),
            userId,
            name);
    }

    public static UserCollection Rehydrate(UserCollectionId id, UserId userId, string name)
    {
        return new UserCollection(id, userId, name);
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty", nameof(newName));
        Name = newName;
    }
}
