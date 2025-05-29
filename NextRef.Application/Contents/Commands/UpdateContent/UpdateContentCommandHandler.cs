using MediatR;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Commands.UpdateContent;
public class UpdateContentHandler : IRequestHandler<UpdateContentCommand, ContentDto>
{
    private readonly IContentRepository _repository;

    public UpdateContentHandler(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ContentDto> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        var content = await _repository.GetByIdAsync(request.Id);
        if (content == null) throw new KeyNotFoundException("Content not found");

        content.Update(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.UpdateAsync(content);

        return ContentMapper.ToDto(content);
    }
}
