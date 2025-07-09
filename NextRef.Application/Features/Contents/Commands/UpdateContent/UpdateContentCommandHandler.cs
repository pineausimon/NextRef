using MediatR;
using NextRef.Application.Caching;
using NextRef.Application.Features.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Features.Contents.Commands.UpdateContent;
internal class UpdateContentHandler : IRequestHandler<UpdateContentCommand, ContentDto>
{
    private readonly IContentRepository _repository;
    private readonly ICacheService _cacheService;

    public UpdateContentHandler(IContentRepository repository, ICacheService cacheService)
    {
        _repository = repository;
        _cacheService = cacheService;
    }

    public async Task<ContentDto> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        var content = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (content == null) throw new KeyNotFoundException("Content not found");

        content.Update(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.UpdateAsync(content, cancellationToken);

        await _cacheService.RemoveByPatternAsync("content_search:*");

        return ContentMapper.ToDto(content);
    }
}
