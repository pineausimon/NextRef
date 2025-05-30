using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Application.UserCollections.Models;
public record UserCollectionDto
{
    public UserCollectionId Id { get; set; }
    public UserId UserId { get; set; }
    public string Name { get; set; }

    public static UserCollectionDto FromDomain(UserCollection collection)
        => new ()
        {
            Id = collection.Id,
            UserId = collection.UserId,
            Name = collection.Name
        };
}
