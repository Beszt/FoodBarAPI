using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Dtos;
using FoodBarAPI.Application.Queries;
using FoodBarAPI.Presentation.Settings;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodBarAPI.Presentation.Controllers;

[SwaggerTag("")]
public class LoginController(IMediator _mediator, JwtSettings _settings) : Controller
{
    [SwaggerOperation("Authentication request that produce JWT bearer")]
    [SwaggerResponse(200, "Body with JWT bearer")]
    [SwaggerResponse(400, "Incorrect body format or wrong credentials")]
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] UserDto login)
    {
        if (login == null)
            return BadRequest("Incorrect data");

        var jwt = await _mediator.Send(new LoginQuery
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
