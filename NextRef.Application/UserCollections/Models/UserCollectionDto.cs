using NextRef.Domain.UserCollections.Models;

namespace NextRef.Application.UserCollections.Models;
public record UserCollectionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }

    public static UserCollectionDto FromDomain(UserCollection collection)
        => new ()
        {
            Id = collection.Id,
            UserId = collection.UserId,
            Name = collection.Name
        };
}
