using MediatR;
using NextRef.Application.Contents.Models;

namespace NextRef.Application.Contents.Commands.CreateContent;

public record CreateContentCommand(
    string Title,
    string Type,
    DateTime PublishedAt,
    string? Description,
    List<ContributionWithExistingContributorDto> ExistingContributions,
    List<ContributionWithNewContributorDto> NewContributions
) : IRequest<Guid>;