using PizzaStoreApp.Interfaces;
using PizzaStoreApp.Models;

namespace PizzaStoreApp.Services
{
    public class PizzaService : IPizzaService
    {
        IRepository<int, Pizza> _pizzaRepository;
        IRepository<int, PizzaImages> _imagesRepository;


        public PizzaService(IRepository<int, Pizza> repository,IRepository<int,PizzaImages> imagesRepository)
        {
            _pizzaRepository = repository;
            _imagesRepository = imagesRepository;
        }
        public List<Pizza> GetAllPizzas()
        {
            var pizzas = _pizzaRepository.GetAll();
            return pizzas;
        }
    }
}
