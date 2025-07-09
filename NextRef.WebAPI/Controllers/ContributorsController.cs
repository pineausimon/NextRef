using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Features.Contributors.Queries.SearchContributors;

namespace NextRef.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContributorsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContributorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchContributorsQuery query)
    {
        var results = await _mediator.Send(query);
        return Ok(results);
    }
}
