using EFWebApiApp.Interfaces;
using EFWebApiApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommandController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductCommandController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO productDto)
        {
            var product = await _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(AddProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}/price")]
        public async Task<ActionResult<ProductDTO>> UpdateProductPrice(int id, [FromBody] double price)
        {
            var updatedProduct = await _productService.UpdateProductPrice(id, price);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }
    }
}
