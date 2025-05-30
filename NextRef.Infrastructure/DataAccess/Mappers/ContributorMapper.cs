using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContributorMapper
{
    //public static ContributorEntity FromDomain(Contributor domain)
    //{
    //    return new ContributorEntity
    //    {
    //        Id = domain.Id,
    //        FullName = domain.FullName,
    //        Bio = domain.Bio,
    //        CreatedAt = DateTime.UtcNow,
    //        UpdatedAt = DateTime.UtcNow
    //    };
    //}

    public static Contributor ToDomain(this ContributorEntity entity)
    {
        return Contributor.Rehydrate((ContributorId)entity.Id, entity.FullName, entity.Bio);
    }
}
