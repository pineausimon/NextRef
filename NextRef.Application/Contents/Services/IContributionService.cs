using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Services;
public interface IContributionService
{
    Task AddContributionsAsync(
        ContentId contentId,
        List<ContributionWithExistingContributorDto> existings,
        List<ContributionWithNewContributorDto> news,
        CancellationToken cancellationToken);
}

public class ContributionService : IContributionService
{
    private readonly IContributorRepository _contributorRepo;
    private readonly IContributionRepository _contributionRepo;

    public ContributionService(
        IContributorRepository contributorRepo,
        IContributionRepository contributionRepo)
    {
        _contributorRepo = contributorRepo;
        _contributionRepo = contributionRepo;
    }

    public async Task AddContributionsAsync(
        ContentId contentId,
        List<ContributionWithExistingContributorDto> existings,
        List<ContributionWithNewContributorDto> news,
        CancellationToken cancellationToken)
    {
        foreach (var dto in existings)
        {
            var contribution = Contribution.Create(dto.ContributorId, contentId, dto.Role);
            await _contributionRepo.AddAsync(contribution, cancellationToken);
        }

        foreach (var dto in news)
        {
            var existingContributor = await _contributorRepo.GetByFullNameAsync(dto.FullName.Trim(), cancellationToken);

            Contributor contributor;

            if (existingContributor is not null)
            {
                contributor = existingContributor;
            }
            else
            {
                contributor = Contributor.Create(dto.FullName.Trim(), "");
                await _contributorRepo.AddAsync(contributor, cancellationToken);
            }

            var contribution = Contribution.Create(contributor.Id, contentId, dto.Role);
            await _contributionRepo.AddAsync(contribution, cancellationToken);
        }
    }
}
