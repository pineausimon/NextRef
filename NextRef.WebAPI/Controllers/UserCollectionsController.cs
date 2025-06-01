using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.UserCollections.Commands.AddContentToCollection;
using NextRef.Application.UserCollections.Commands.CreateCollection;
using NextRef.Application.UserCollections.Queries.GetUserCollections;
using NextRef.Domain.Core.Ids;

namespace NextRef.WebAPI.Controllers;
[Route("api/collections")]
[ApiController]
public class UserCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserCollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var userCollections = await _mediator.Send(new GetUserCollectionsQuery((UserId)userId));
        if (userCollections == null) return NotFound();
        return Ok(userCollections);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCollectionCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = newId }, new { id = newId });
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost("{collectionId}/items")]
    public async Task<IActionResult> AddContentToCollection(Guid collectionId, AddContentToCollectionCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddContentToCollection), new { id = newId }, new { id = newId });
    }
}
