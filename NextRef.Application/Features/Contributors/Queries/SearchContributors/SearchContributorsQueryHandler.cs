using MediatR;
using NextRef.Application.Features.Contributors.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Features.Contributors.Queries.SearchContributors;
internal class SearchContributorsQueryHandler : IRequestHandler<SearchContributorsQuery, IReadOnlyList<ContributorDto>>
{
    private readonly IContributorRepository _contributorRepository;
    public SearchContributorsQueryHandler(IContributorRepository contributorRepository)
    {
        _contributorRepository = contributorRepository;
    }
    public async Task<IReadOnlyList<ContributorDto>> Handle(SearchContributorsQuery request, CancellationToken cancellationToken)
    {
        var contributors = await _contributorRepository.SearchAsync(request.Keyword, cancellationToken);

        return contributors.Select(ContributorDto.FromDomain).ToList();
    }
}
