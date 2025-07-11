﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Features.UserCollections.Commands.AddContentToCollection;
using NextRef.Application.Features.UserCollections.Commands.CreateCollection;
using NextRef.Application.Features.UserCollections.Queries.GetUserCollections;
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
        var newCollection = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = newCollection.Id }, newCollection);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost("{collectionId}/items")]
    public async Task<IActionResult> AddContentToCollection(Guid collectionId, AddContentToCollectionCommand command)
    {
        var newItem = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddContentToCollection), new { id = newItem.Id }, newItem);
    }
}
