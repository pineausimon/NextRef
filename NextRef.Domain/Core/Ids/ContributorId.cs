namespace NextRef.Domain.Core.Ids;
public readonly struct ContributorId : IEquatable<ContributorId>
{
    public Guid Value { get; }

    public ContributorId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ContributorId cannot be empty", nameof(value));

        Value = value;
    }

    public static ContributorId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is ContributorId other && Equals(other);

    public bool Equals(ContributorId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ContributorId left, ContributorId right) => left.Equals(right);

    public static bool operator !=(ContributorId left, ContributorId right) => !(left == right);

    public static explicit operator Guid(ContributorId id) => id.Value;

    public static explicit operator ContributorId(Guid guid) => new(guid);
}
