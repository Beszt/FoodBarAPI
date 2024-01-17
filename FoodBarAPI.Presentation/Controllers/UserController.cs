
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;
using FoodBarAPI.Application.Validators;
using Swashbuckle.AspNetCore.Annotations;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Presentation.Controllers;

[Authorize (Roles = "admin")]
public class UserController(IServiceProvider _servicesCollection, IMediator _mediator) : Controller
{
    [SwaggerOperation("Create new user")]
    [SwaggerResponse(201, "User created")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
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

    [SwaggerOperation("Get user determined by it's login")]
    [SwaggerResponse(200, "JSON with user info")]
    [SwaggerResponse(404, "User not found")]
    [HttpGet("/user/{login}")]
    public async Task<IActionResult> Get(string login)
    {
        var user = await _mediator.Send(new GetUserQuery(login));

        if (user == null)
            return NotFound();
            
        return Ok(user);
    }

    [SwaggerOperation("Edit exististing user")]
    [SwaggerResponse(200, "User updated")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
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

    [SwaggerOperation("Delete user determined it's login")]
    [SwaggerResponse(200, "User deleted")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpDelete("/user/{login}")]
    public async Task<IActionResult> Delete(string login)
    {
        var command = new DeleteUserCommand{Login = login};

        var validator = _servicesCollection.GetRequiredService<DeleteUserCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return Ok();
    }
}
