using MediatR;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Commands.UpdateContent;
public class UpdateContentCommand : IRequest<ContentDto>
{
    public ContentId Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime PublishedAt { get; init; }
}

