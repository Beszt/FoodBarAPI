using Microsoft.AspNetCore.Mvc;

namespace FoodBarAPI.Controllers
{
    public class BarcodeController : Controller
    {
        [HttpGet("/barcode")]
        public IActionResult Index()
        {
            return BadRequest();
        }

        [HttpGet("/barcode/{id}")]
        public IActionResult Index(int id)
        {
            return Ok();
        }
    }
}