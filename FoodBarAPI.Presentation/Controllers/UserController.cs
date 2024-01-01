using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Application.Queries;
using FoodBarAPI.Presentation.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodBarAPI.Presentation.Controllers;

public class UserController(IMediator _mediator, JwtSettings settings) : Controller
{
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var jwt = await _mediator.Send(new LoginUserQuery(login.Login, login.Password, settings.Key, settings.Issuer, settings.ExpireInDays));

        if (jwt == null)
            return BadRequest("Wrong username or password!");

        return Ok(jwt);
    }
}
