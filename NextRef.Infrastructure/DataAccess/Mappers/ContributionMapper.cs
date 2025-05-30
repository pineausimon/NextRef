using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContributionMapper
{
    //public static ContributionEntity FromDomain(Contribution domain)
    //{
    //    return new ContributionEntity
    //    {
    //        Id = domain.Id,
    //        ContributorId = domain.ContributorId,
    //        ContentId = domain.ContentId,
    //        Role = domain.Role,
    //        CreatedAt = DateTime.UtcNow,
    //        UpdatedAt = DateTime.UtcNow
    //    };
    //}

    public static Contribution ToDomain(this ContributionEntity entity)
    {
        return Contribution.Rehydrate((ContributionId)entity.Id, (ContributorId)entity.ContributorId, (ContentId)entity.ContentId, entity.Role);
    }
}

