namespace NextRef.Domain.Core.Ids;
public readonly struct UserCollectionItemId : IEquatable<UserCollectionItemId>
{
    public Guid Value { get; }

    public UserCollectionItemId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserCollectionItemId cannot be empty", nameof(value));

        Value = value;
    }

    public static UserCollectionItemId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is UserCollectionItemId other && Equals(other);

    public bool Equals(UserCollectionItemId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(UserCollectionItemId left, UserCollectionItemId right) => left.Equals(right);

    public static bool operator !=(UserCollectionItemId left, UserCollectionItemId right) => !(left == right);

    public static explicit operator Guid(UserCollectionItemId id) => id.Value;

    public static explicit operator UserCollectionItemId(Guid guid) => new(guid);
}
