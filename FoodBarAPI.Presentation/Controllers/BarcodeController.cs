using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;
using Microsoft.AspNetCore.Authorization;

namespace FoodBarAPI.Controllers
{
    [Authorize]
    public class BarcodeController(IServiceProvider _servicesCollection, IMediator _mediator) : Controller
    {
        [AllowAnonymous]
        [HttpGet("/barcode")]
        public IActionResult Index()
        {
            return BadRequest();
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("/barcode")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var validator = _servicesCollection.GetRequiredService<CreateProductCommandValidator>();
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            command.UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _mediator.Send(command);

            return StatusCode(201); // Created() gives 204 - Bug?
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("/barcode/{barcode}")]
        public async Task<IActionResult> Get(long barcode)
        {
            var product = await _mediator.Send(new GetProductQuery(barcode));

            if (product == null)
                return NotFound();
                
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/barcode")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var validator = _servicesCollection.GetRequiredService<UpdateProductCommandValidator>();
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("/barcode/{barcode}")]
        public async Task<IActionResult> Delete(long barcode)
        {
            var command = new DeleteProductCommand(barcode);

            var validator = _servicesCollection.GetRequiredService<DeleteProductCommandValidator>();
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _mediator.Send(command);

            return Ok();
        }
    }
}