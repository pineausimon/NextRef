using MediatR;

namespace NextRef.Application.Contents.Commands.DeleteContent;
public record DeleteContentCommand(Guid Id) : IRequest;