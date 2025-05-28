using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Users.Commands.LoginUser;
using NextRef.Application.Users.Commands.RegisterUser;
using NextRef.Application.Users.Commands.UpdateUser;
using NextRef.Application.Users.Queries.GetUser;

namespace NextRef.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var success = await _mediator.Send(command);
        if (!success)
            return BadRequest("Registration failed");
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        if (token == null)
            return Unauthorized();
        return Ok(new { Token = token });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _mediator.Send(new GetUserQuery(id));
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        var user = await _mediator.Send(command);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}
