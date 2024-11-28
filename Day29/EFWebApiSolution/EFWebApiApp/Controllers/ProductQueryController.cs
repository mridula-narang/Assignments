using EFWebApiApp.Interfaces;
using EFWebApiApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductQueryController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductQueryController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
