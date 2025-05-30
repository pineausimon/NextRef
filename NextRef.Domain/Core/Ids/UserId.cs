namespace NextRef.Domain.Core.Ids;
public readonly struct UserId : IEquatable<UserId>
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(value));

        Value = value;
    }

    public static UserId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is UserId other && Equals(other);

    public bool Equals(UserId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(UserId left, UserId right) => left.Equals(right);

    public static bool operator !=(UserId left, UserId right) => !(left == right);

    public static explicit operator Guid(UserId id) => id.Value;

    public static explicit operator UserId(Guid guid) => new(guid);
}
