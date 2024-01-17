using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Validators;
using FoodBarAPI.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using FoodBarAPI.Application.Dtos;

namespace FoodBarAPI.Controllers;

[Authorize]
public class BarcodeController(IServiceProvider _servicesCollection, IMediator _mediator) : Controller
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpGet("/barcode")]
    public IActionResult Index()
    {
        return BadRequest("There are no index Bro");
    }

    [SwaggerOperation("Create new product")]
    [SwaggerResponse(201, "Product created")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
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

    [SwaggerOperation("Get product determined by EAN code")]
    [SwaggerResponse(200, "JSON with product info")]
    [SwaggerResponse(404, "Product not found")]
    [HttpGet("/barcode/{barcode}")]
    public async Task<IActionResult> Get(long barcode)
    {
        var product = await _mediator.Send(new GetProductQuery{Barcode = barcode});

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [SwaggerOperation("Edit exististing product")]
    [SwaggerResponse(200, "Product updated")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
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

    [SwaggerOperation("Delete product determined EAN code")]
    [SwaggerResponse(200, "Product deleted")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
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