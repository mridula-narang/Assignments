using EFWebApiApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // POST: api/ProductCommand/{productId}/images
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddProductImages(int productId, [FromBody] List<string> imageUrls)
        {
            try
            {
                if (imageUrls == null || imageUrls.Count == 0)
                {
                    return BadRequest("No images provided.");
                }

                await _productImageService.AddImagesToProduct(productId, imageUrls);
                return Ok("Images added successfully.");
            }
            catch (Exception ex)
            {
                // Log the error and return a meaningful response to the client
                Console.WriteLine($"An error occurred while adding images: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while adding images.");
            }
        }
    }
}
