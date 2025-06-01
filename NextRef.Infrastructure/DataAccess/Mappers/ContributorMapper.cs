using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Infrastructure.DataAccess.Entities;

namespace NextRef.Infrastructure.DataAccess.Mappers;
public static class ContributorMapper
{
    public static Contributor ToDomain(this ContributorEntity entity)
    {
        return Contributor.Rehydrate((ContributorId)entity.Id, entity.FullName, entity.Bio);
    }
}
