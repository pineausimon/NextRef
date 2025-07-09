using MediatR;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Commands.DeleteContent;
public record DeleteContentCommand(ContentId Id) : IRequest;