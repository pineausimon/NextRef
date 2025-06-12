using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Contents.Commands.CreateContentMention;

namespace NextRef.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContentMentionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContentMentionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateContentMentionCommand command)
    {
        var newMention = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = newMention.Id }, newMention);
    }
}
