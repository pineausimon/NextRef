using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.UserCollections.Commands.CreateCollection;
using NextRef.Application.UserCollections.Queries.GetUserCollections;
using NextRef.Domain.Core.Ids;

namespace NextRef.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserCollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var content = await _mediator.Send(new GetUserCollectionsQuery((UserId)id));
        if (content == null) return NotFound();
        return Ok(content);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCollectionCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = newId }, new { id = newId });
    }
}
