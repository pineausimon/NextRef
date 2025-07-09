using MediatR;
using NextRef.Application.Features.Contents.Models;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Features.Contents.Commands.CreateContentMention;
internal class CreateContentMentionCommandHandler : IRequestHandler<CreateContentMentionCommand, ContentMentionDto>
{
    private readonly IContentMentionRepository _contentMentionRepository;
    public CreateContentMentionCommandHandler(IContentMentionRepository contentMentionRepository)
    {
        _contentMentionRepository = contentMentionRepository;
    }
    public async Task<ContentMentionDto> Handle(CreateContentMentionCommand request, CancellationToken cancellationToken)
    {
        var contentMention = ContentMention.Create(request.SourceContentId, request.TargetContentId, request.Context);

        await _contentMentionRepository.AddAsync(contentMention, cancellationToken);
        return ContentMentionDto.FromDomain(contentMention);
    }
}
