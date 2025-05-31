using MediatR;
using NextRef.Application.Contents.Models;

namespace NextRef.Application.Contents.Queries.SearchContents;

public record SearchContentsQuery(
    string? Keyword,
    string? SortBy = "createdat", // TODO : make this an enum
    int? Limit = 20,
    int Page = 1) : IRequest<IReadOnlyList<ContentDto>>;
