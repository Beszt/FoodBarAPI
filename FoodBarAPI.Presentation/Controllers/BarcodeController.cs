using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace FoodBarAPI.Controllers
{
    public class BarcodeController(IServiceProvider _servicesCollection, IMediator _mediator) : Controller
    {
        [HttpGet("/barcode")]
        public IActionResult Index()
        {
            return BadRequest();
        }

        [HttpPost("/barcode")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var validator = _servicesCollection.GetRequiredService<CreateProductCommandValidator>();
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("/barcode/{barcode}")]
        public async Task<IActionResult> Get(long barcode)
        {
            var product = await _mediator.Send(new GetProductQuery(barcode));

            if (product == null)
                return NotFound();
                
            return Ok(product);
        }

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