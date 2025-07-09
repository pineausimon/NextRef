using MediatR;
using NextRef.Application.Features.Contributors.Models;

namespace NextRef.Application.Features.Contributors.Queries.SearchContributors;

public record SearchContributorsQuery(string Keyword) : IRequest<IReadOnlyList<ContributorDto>>;
