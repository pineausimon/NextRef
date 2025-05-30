using MediatR;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Commands.CreateContentMention;
internal class CreateContentMentionCommandHandler : IRequestHandler<CreateContentMentionCommand, ContentMentionId>
{
    private readonly IContentMentionRepository _contentMentionRepository;
    public CreateContentMentionCommandHandler(IContentMentionRepository contentMentionRepository)
    {
        _contentMentionRepository = contentMentionRepository;
    }
    public async Task<ContentMentionId> Handle(CreateContentMentionCommand request, CancellationToken cancellationToken)
    {
        var contentMention = ContentMention.Create(request.SourceContentId, request.TargetContentId, request.Context);

        await _contentMentionRepository.AddAsync(contentMention);
        return contentMention.Id;
    }
}
