using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;

namespace NextRef.Application.Features.UserCollections.Models;

public record UserCollectionItemDto(
    UserCollectionItemId Id,
    UserCollectionId CollectionId,
    ContentId ContentId,
    string Status,
    DateTime AddedAt)
{

    public static UserCollectionItemDto FromDomain(UserCollectionItem item)
        => new(item.Id, item.CollectionId, item.ContentId, item.Status, item.AddedAt);
}
