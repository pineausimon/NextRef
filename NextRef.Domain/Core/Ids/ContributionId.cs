namespace NextRef.Domain.Core.Ids;
public readonly struct ContributionId : IEquatable<ContributionId>
{
    public Guid Value { get; }

    public ContributionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ContributionId cannot be empty", nameof(value));

        Value = value;
    }

    public static ContributionId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public override bool Equals(object? obj) => obj is ContributionId other && Equals(other);

    public bool Equals(ContributionId other) => Value.Equals(other.Value);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ContributionId left, ContributionId right) => left.Equals(right);

    public static bool operator !=(ContributionId left, ContributionId right) => !(left == right);

    public static explicit operator Guid(ContributionId id) => id.Value;

    public static explicit operator ContributionId(Guid guid) => new(guid);
}
