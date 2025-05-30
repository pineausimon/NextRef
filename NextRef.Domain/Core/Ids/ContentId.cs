namespace NextRef.Domain.Core.Ids;
public readonly struct ContentId : IEquatable<ContentId>
{
    public Guid Value { get; }

    public ContentId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ContentId cannot be empty", nameof(value));

        Value = value;
    }

    public static ContentId New() => new (Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is ContentId other && Equals(other);

    public bool Equals(ContentId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ContentId left, ContentId right) => left.Equals(right);

    public static bool operator !=(ContentId left, ContentId right) => !(left == right);

    public static explicit operator Guid(ContentId id) => id.Value;

    public static explicit operator ContentId(Guid guid) => new (guid);
}
