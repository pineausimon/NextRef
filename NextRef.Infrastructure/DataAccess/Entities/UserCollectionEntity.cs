namespace NextRef.Infrastructure.DataAccess.Entities;
public class UserCollectionEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
}
