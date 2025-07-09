using MediatR;
using NextRef.Application.Features.Contents.Models;

namespace NextRef.Application.Features.Contents.Commands.CreateContent;

public record CreateContentCommand(
    string Title,
    string Type,
    DateTime PublishedAt,
    string? Description,
    List<ContributionWithExistingContributorDto> ExistingContributions,
    List<ContributionWithNewContributorDto> NewContributions
) : IRequest<ContentDto>;