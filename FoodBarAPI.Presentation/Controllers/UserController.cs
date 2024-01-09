
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;
using FoodBarAPI.Application.Validators;
using FoodBarAPI.Presentation.Settings;

namespace FoodBarAPI.Presentation.Controllers;

[Authorize (Roles = "admin")]
public class UserController(IServiceProvider _servicesCollection, IMediator _mediator, JwtSettings settings) : Controller
{
    [HttpPost("/user")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var validator = _servicesCollection.GetRequiredService<CreateUserCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return StatusCode(201); // Created() gives 204 - Bug?
    }

    [AllowAnonymous]
    [HttpGet("/user/{login}")]
    public async Task<IActionResult> Get(string login)
    {
        var user = await _mediator.Send(new GetUserQuery(login));

        if (user == null)
            return NotFound();
            
        return Ok(user);
    }

    [HttpPut("/user")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var validator = _servicesCollection.GetRequiredService<UpdateUserCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete("/user/{login}")]
    public async Task<IActionResult> Delete(string login)
    {
        var command = new DeleteUserCommand{Login = login};

        var validator = _servicesCollection.GetRequiredService<DeleteUserCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return Ok(); // Created() gives 204 - Bug?
    }
}
