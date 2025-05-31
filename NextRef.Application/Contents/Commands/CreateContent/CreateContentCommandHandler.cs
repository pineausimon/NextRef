using MediatR;
using NextRef.Application.Caching;
using NextRef.Application.Contents.Services;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Commands.CreateContent;

internal class CreateContentHandler : IRequestHandler<CreateContentCommand, ContentId>
{
    private readonly IContentRepository _repository;
    private readonly IContributionService _contributionService;
    private readonly ICacheService _cacheService;

    public CreateContentHandler(IContentRepository repository, IContributionService contributionService, ICacheService cacheService)
    {
        _repository = repository;
        _contributionService = contributionService;
        _cacheService = cacheService;
    }

    public async Task<ContentId> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = Content.Create(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.AddAsync(content);


        await _contributionService.AddContributionsAsync(
            content.Id, request.ExistingContributions, request.NewContributions);

        await _cacheService.RemoveByPatternAsync("content_search:*");

        return content.Id;
    }
}
