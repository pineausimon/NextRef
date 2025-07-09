using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Features.Contents.Commands.DeleteContent;
using NextRef.Application.Features.Contents.Queries.GetContentById;
using NextRef.Application.Features.Contents.Commands.CreateContent;
using NextRef.Application.Features.Contents.Commands.UpdateContent;
using NextRef.Application.Features.Contents.Queries.SearchContents;
using NextRef.Domain.Core;
using NextRef.Domain.Core.Ids;

namespace NextRef.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchContentsQuery query)
    {
        var results = await _mediator.Send(query);
        return Ok(results);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var content = await _mediator.Send(new GetContentByIdQuery((ContentId)id));
        if (content == null) return NotFound();
        return Ok(content);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateContentCommand command)
    {
        var newContent = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = newContent.Id }, newContent);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateContentCommand command)
    {
        if ((ContentId) id != command.Id) 
            return BadRequest("ID mismatch");

        var updatedContent = await _mediator.Send(command);
        return Ok(updatedContent);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContentCommand((ContentId)id));
        return NoContent();
    }
}