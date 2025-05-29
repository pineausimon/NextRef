using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Services;
public interface IContributionService
{
    Task AddContributionsAsync(
        Guid contentId,
        List<ContributionWithExistingContributorDto> existings,
        List<ContributionWithNewContributorDto> news);
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
        Guid contentId,
        List<ContributionWithExistingContributorDto> existings,
        List<ContributionWithNewContributorDto> news)
    {
        foreach (var dto in existings)
        {
            var contribution = Contribution.Create(contentId, dto.ContributorId, dto.Role);
            await _contributionRepo.AddAsync(contribution);
        }

        // 2. Ajouter les nouveaux contributeurs + leurs contributions
        foreach (var dto in news)
        {
            var contributor = Contributor.Create(dto.FullName, ""); 
            await _contributorRepo.AddAsync(contributor);

            var contribution = Contribution.Create(contentId, contributor.Id, dto.Role);
            await _contributionRepo.AddAsync(contribution);
        }
    }
}
