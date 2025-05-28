namespace NextRef.Infrastructure.DataAccess.Entities;
public class ContributionEntity : BaseEntity
{
    public Guid ContributorId { get; set; }
    public Guid ContentId { get; set; }
    public string Role { get; set; } = string.Empty;
}
