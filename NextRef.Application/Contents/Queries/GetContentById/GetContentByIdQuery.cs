using MediatR;
using NextRef.Application.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Contents.Queries.GetContentById;
public record GetContentByIdQuery(ContentId Id) : IRequest<ContentDto?>;
