using MediatR;
using NextRef.Application.Caching;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Commands.DeleteContent;
internal class DeleteContentHandler : IRequestHandler<DeleteContentCommand>
{
    private readonly IContentRepository _repository;
    private readonly ICacheService _cacheService;

    public DeleteContentHandler(IContentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task Handle(DeleteContentCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        await _cacheService.RemoveByPatternAsync("content_search:*");
    }
}