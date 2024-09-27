using Microsoft.AspNetCore.Mvc;
using PizzaStoreApp.Interfaces;
using PizzaStoreApp.Services;

namespace PizzaStoreApp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }
        public IActionResult Index()
        {
            try
            {
                var pizzas = _pizzaService.GetAllPizzas();
                return View(pizzas);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "There was an error retriving the pizzas";
                return View();
            }
        }
    }
}
