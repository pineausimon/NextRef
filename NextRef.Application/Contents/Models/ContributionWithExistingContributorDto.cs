using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Models;
public record ContributionWithExistingContributorDto(ContributorId ContributorId, string Role);
