namespace NextRef.Infrastructure.DataAccess.Entities;
public class ContributorEntity : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}
