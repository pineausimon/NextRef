using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Commands.DeleteContent;
using NextRef.Application.Contents.Commands.UpdateContent;
using NextRef.Application.Contents.Queries.GetContentById;
using NextRef.Domain.Core;

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

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var content = await _mediator.Send(new GetContentByIdQuery(id));
        if (content == null) return NotFound();
        return Ok(content);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateContentCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = newId }, new { id = newId });
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateContentCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch");

        try
        {
            await _mediator.Send(command);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        return NoContent();
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContentCommand(id));
        return NoContent();
    }
}