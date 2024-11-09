using Microsoft.AspNetCore.Mvc;
using FirstWebApp.Models;
namespace FirstWebApp.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Fraganote Island Water", Price = 5000, Description = "Perfume", Image = "/images/1.jpg", Quantity = 10 },
            new Product { Id = 2, Name = "Sangria", Price = 2000, Description = "Fashion Jewellery", Image = "/images/2.jpg", Quantity = 10 },
        };
        public IActionResult Index()
        {
            return View(products);
        }
    }
}
