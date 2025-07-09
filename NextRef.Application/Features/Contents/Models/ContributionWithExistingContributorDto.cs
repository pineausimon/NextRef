using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Models;
public record ContributionWithExistingContributorDto(ContributorId ContributorId, string Role);
