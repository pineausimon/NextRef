using MediatR;
using NextRef.Application.Contents.Models;

namespace NextRef.Application.Contents.Queries.SearchContents;

public record SearchContentsQuery(
    string? Keyword,
    string? SortBy = "createdat", // TODO : make this an enum
    int? Limit = 20) : IRequest<IReadOnlyList<ContentDto>>;
