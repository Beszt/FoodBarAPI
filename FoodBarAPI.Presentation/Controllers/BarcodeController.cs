using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Validators;
using FoodBarAPI.Application.Queries;
using Microsoft.AspNetCore.Authorization;

namespace FoodBarAPI.Controllers;

[Authorize]
public class BarcodeController(IServiceProvider _servicesCollection, IMediator _mediator) : Controller
{
    [AllowAnonymous]
    [HttpGet("/barcode")]
    public IActionResult Index()
    {
        return BadRequest("There are no index Bro");
    }

    [HttpPost("/barcode")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        command.UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var validator = _servicesCollection.GetRequiredService<CreateProductCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return StatusCode(201); // Created() gives 204 - Bug?
    }

    [HttpGet("/barcode/{barcode}")]
    public async Task<IActionResult> Get(long barcode)
    {
        var product = await _mediator.Send(new GetProductQuery{Barcode = barcode});

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPut("/barcode")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        command.UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var validator = _servicesCollection.GetRequiredService<UpdateProductCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete("/barcode/{barcode}")]
    public async Task<IActionResult> Delete(long barcode)
    {
        var command = new DeleteProductCommand()
        {
            Barcode = barcode,
            UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        var validator = _servicesCollection.GetRequiredService<DeleteProductCommandValidator>();
        var result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(command);

        return Ok();
    }
}