using MediatR;
using NextRef.Application.Caching;
using NextRef.Application.Features.Contents.Models;
using NextRef.Application.Features.Contents.Services;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Features.Contents.Commands.CreateContent;

internal class CreateContentHandler : IRequestHandler<CreateContentCommand, ContentDto>
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

    public async Task<ContentDto> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = Content.Create(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.AddAsync(content, cancellationToken);


        await _contributionService.AddContributionsAsync(
            content.Id, request.ExistingContributions, request.NewContributions, cancellationToken);

        await _cacheService.RemoveByPatternAsync("content_search:*");

        return ContentMapper.ToDto(content);
    }
}
