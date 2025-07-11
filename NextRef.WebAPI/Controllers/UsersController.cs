﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextRef.Application.Features.Users.Commands.LoginUser;
using NextRef.Application.Features.Users.Commands.RegisterUser;
using NextRef.Application.Features.Users.Commands.UpdateUser;
using NextRef.Application.Features.Users.Queries.GetUser;
using NextRef.Domain.Core.Ids;

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

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var token = await _mediator.Send(command);
        if (token == null)
            return BadRequest("Registration failed");

        return Ok(new { Token = token });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var token = await _mediator.Send(command);
        if (token == null)
            return Unauthorized(); 
        
        return Ok(new { Token = token });
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _mediator.Send(new GetUserQuery((UserId)id));
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [Authorize(Policy = "UserOrAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand command)
    {
        if ((UserId)id != command.Id)
            return BadRequest("Id mismatch");

        var user = await _mediator.Send(command);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}
