using NextRef.Domain.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contributors.Models;

public record ContributorDto(ContributorId Id, string FullName, string? Bio)
{
    public static ContributorDto FromDomain(Contributor contributor)
        => new(contributor.Id, contributor.FullName, contributor.Bio);
}
