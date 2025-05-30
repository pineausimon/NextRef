namespace NextRef.Domain.Core.Ids;
public readonly struct UserCollectionId : IEquatable<UserCollectionId>
{
    public Guid Value { get; }

    public UserCollectionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserCollectionId cannot be empty", nameof(value));

        Value = value;
    }

    public static UserCollectionId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is UserCollectionId other && Equals(other);

    public bool Equals(UserCollectionId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(UserCollectionId left, UserCollectionId right) => left.Equals(right);

    public static bool operator !=(UserCollectionId left, UserCollectionId right) => !(left == right);

    public static explicit operator Guid(UserCollectionId id) => id.Value;

    public static explicit operator UserCollectionId(Guid guid) => new(guid);
}
