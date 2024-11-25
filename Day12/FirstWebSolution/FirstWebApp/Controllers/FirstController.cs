using FirstWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp.Controllers
{
    public class FirstController : Controller
    {

        public IActionResult Index()
        {
            ViewData["Customer Name"] = "James Potter";
            ViewData["Customer Age"] = 30;

            ViewData["Customer"] = new Customer { Id = 1, Name = "James Potter", Age = 30 };
            ViewBag.Customer = new Customer { Id = 2, Name = "Lily Potter", Age = 25 };
            return View();
        }

        public IActionResult ViewCustomerData()
        {
            Customer customer = new Customer { Id = 1, Name = "James Potter", Age = 30 };
            return View(customer);
        }
    }
}
