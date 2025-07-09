using MediatR;
using NextRef.Application.Features.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Commands.UpdateContent;

public record UpdateContentCommand(
    ContentId Id,
    string Title,
    string Type,
    DateTime PublishedAt,
    string? Description
) : IRequest<ContentDto>;

