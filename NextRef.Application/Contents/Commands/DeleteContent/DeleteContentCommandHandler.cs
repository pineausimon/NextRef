using MediatR;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Contents.Commands.DeleteContent;
internal class DeleteContentHandler : IRequestHandler<DeleteContentCommand>
{
    private readonly IContentRepository _repository;

    public DeleteContentHandler(IContentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteContentCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
    }
}