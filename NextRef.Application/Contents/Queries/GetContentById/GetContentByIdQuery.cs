using MediatR;
using NextRef.Application.Contents.Models;

namespace NextRef.Application.Contents.Queries.GetContentById;
public record GetContentByIdQuery(Guid Id) : IRequest<ContentDto?>;
