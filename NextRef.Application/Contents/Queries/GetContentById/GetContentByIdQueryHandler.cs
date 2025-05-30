using MediatR;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Queries.GetContentById;
internal class GetContentByIdHandler : IRequestHandler<GetContentByIdQuery, ContentDto?>
{
    private readonly IContentRepository _repository;

    public GetContentByIdHandler(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ContentDto?> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
    {
        var content = await _repository.GetByIdAsync(request.Id);

        return content == null 
            ? null 
            : ContentMapper.ToDto(content);
    }
}