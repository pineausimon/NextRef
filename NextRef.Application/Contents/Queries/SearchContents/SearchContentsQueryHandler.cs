using MediatR;
using NextRef.Application.Caching;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Queries.SearchContents;
internal class SearchContentsQueryHandler : IRequestHandler<SearchContentsQuery, IReadOnlyList<ContentDto>>
{
    private readonly IContentRepository _repository;
    private readonly ICacheService _cacheService;

    public SearchContentsQueryHandler(IContentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<IReadOnlyList<ContentDto>> Handle(SearchContentsQuery request, CancellationToken cancellationToken)
    {
        // Génère une clé de cache unique basée sur les paramètres de la requête
        var cacheKey = $"content_search:{request.Keyword}:{request.SortBy}:{request.Limit}:{1}";

        // 1. Cherche dans le cache
        var cached = await _cacheService.GetAsync<IReadOnlyList<ContentDto>>(cacheKey);
        if (cached != null)
            return cached;

        // 2. Sinon, va en base
        var contents = await _repository.SearchAsync(request.Keyword, request.SortBy, request.Limit, request.Page);
        var dtos = contents.Select(ContentMapper.ToDto).ToList();

        // 3. Stocke en cache
        await _cacheService.SetAsync(cacheKey, dtos);

        return dtos;
    }
}