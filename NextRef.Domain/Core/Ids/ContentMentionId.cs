namespace NextRef.Domain.Core.Ids;
public readonly struct ContentMentionId : IEquatable<ContentMentionId>
{
    public Guid Value { get; }

    public ContentMentionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ContentMentionId cannot be empty", nameof(value));

        Value = value;
    }

    public static ContentMentionId New() => new (Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is ContentMentionId other && Equals(other);

    public bool Equals(ContentMentionId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ContentMentionId left, ContentMentionId right) => left.Equals(right);

    public static bool operator !=(ContentMentionId left, ContentMentionId right) => !(left == right);

    public static explicit operator Guid(ContentMentionId id) => id.Value;

    public static explicit operator ContentMentionId(Guid guid) => new (guid);
}
