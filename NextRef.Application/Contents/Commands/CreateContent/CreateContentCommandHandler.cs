using MediatR;
using NextRef.Domain.Contents;

namespace NextRef.Application.Contents.Commands.CreateContent;

public class CreateContentHandler : IRequestHandler<CreateContentCommand, Guid>
{
    private readonly IContentRepository _repository;

    public CreateContentHandler(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = Content.Create(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.AddAsync(content);
        return content.Id;
    }
}
