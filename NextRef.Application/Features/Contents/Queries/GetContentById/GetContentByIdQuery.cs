using MediatR;
using NextRef.Application.Features.Contents.Models;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Features.Contents.Queries.GetContentById;
public record GetContentByIdQuery(ContentId Id) : IRequest<ContentDto?>;
