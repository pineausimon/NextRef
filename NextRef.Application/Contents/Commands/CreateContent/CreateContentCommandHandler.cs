using MediatR;
using NextRef.Application.Contents.Services;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Commands.CreateContent;

internal class CreateContentHandler : IRequestHandler<CreateContentCommand, ContentId>
{
    private readonly IContentRepository _repository;
    private readonly IContributionService _contributionService;

    public CreateContentHandler(IContentRepository repository, IContributionService contributionService)
    {
        _repository = repository;
        _contributionService = contributionService;
    }

    public async Task<ContentId> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = Content.Create(request.Title, request.Type, request.PublishedAt, request.Description);
        await _repository.AddAsync(content);


        await _contributionService.AddContributionsAsync(
            content.Id, request.ExistingContributions, request.NewContributions);

        return content.Id;
    }
}
