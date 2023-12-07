using Microsoft.AspNetCore.Mvc;
using MediatR;
using FoodBarAPI.Application.Commands;
using FoodBarAPI.Application.Queries;

namespace FoodBarAPI.Controllers
{
    public class BarcodeController(IMediator _mediator) : Controller
    {
        [HttpGet("/barcode")]
        public IActionResult Index()
        {
            return BadRequest();
        }

        [HttpPost("/barcode")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("/barcode/{code}")]
        public async Task<IActionResult> Get(long code)
        {
            var product = await _mediator.Send(new GetProductQuery(code));

            return Ok(product);
        }

        [HttpPut("/barcode")]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("/barcode/{code}")]
        public async Task<IActionResult> Delete(long code)
        {
            await _mediator.Send(new DeleteProductCommand(code));

            return Ok();
        }
    }
}