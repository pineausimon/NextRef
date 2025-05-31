using MediatR;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Queries.SearchContents;
internal class SearchContentsQueryHandler : IRequestHandler<SearchContentsQuery, IReadOnlyList<ContentDto>>
{
    private readonly IContentRepository _repository;

    public SearchContentsQueryHandler(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ContentDto>> Handle(SearchContentsQuery request, CancellationToken cancellationToken)
    {
        var contents = await _repository.SearchAsync(request.Keyword, request.SortBy, request.Limit);

        return contents.Select(ContentMapper.ToDto).ToList();
    }
}