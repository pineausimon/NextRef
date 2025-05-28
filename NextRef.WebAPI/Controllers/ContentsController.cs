using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Application.Contents.Commands.DeleteContent;
using NextRef.Application.Contents.Commands.UpdateContent;
using NextRef.Application.Contents.Queries.GetContentById;

namespace NextRef.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var content = await _mediator.Send(new GetContentByIdQuery(id));
        if (content == null) return NotFound();
        return Ok(content);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContentCommand command)
    {
        var newId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = newId }, null);
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContentCommand(id));
        return NoContent();
    }
}