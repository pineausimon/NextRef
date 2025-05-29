namespace NextRef.Domain.UserCollections.Models;

public class UserCollection
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }

    private UserCollection(Guid id, Guid userId, string name)
    {
        Id = id;
        UserId = userId;
        Name = name;
    }

    public static UserCollection Create(Guid userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

        return new UserCollection(
            Guid.NewGuid(),
            userId,
            name);
    }

    public static UserCollection Rehydrate(Guid id, Guid userId, string name)
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
