using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Application.Queries;
using FoodBarAPI.Presentation.Settings;

namespace FoodBarAPI.Presentation.Controllers;

public class LoginController(IMediator _mediator, JwtSettings _settings) : Controller
{
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserDto login)
    {
        if (login == null)
            return BadRequest("Incorrect data");

        var jwt = await _mediator.Send(new LoginUserQuery
        {
            Login = login.Login,
            Password = login.Password,
            JwtKey = _settings.Key,
            JwtIssuer = _settings.Issuer,
            JwtExpire = _settings.ExpireInDays
        });

        if (jwt == null)
            return BadRequest("Wrong username or password!");

        return Ok(jwt);
    }
}
