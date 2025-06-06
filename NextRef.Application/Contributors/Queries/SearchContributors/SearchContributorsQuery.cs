using MediatR;
using NextRef.Application.Contributors.Models;

namespace NextRef.Application.Contributors.Queries.SearchContributors;

public record SearchContributorsQuery(string Keyword) : IRequest<IReadOnlyList<ContributorDto>>;
