using MediatR;

namespace NextRef.Application.Contents.Commands.CreateContent;

public record CreateContentCommand(string Title, string Type, DateTime PublishedAt, string? Description) : IRequest<Guid>;

