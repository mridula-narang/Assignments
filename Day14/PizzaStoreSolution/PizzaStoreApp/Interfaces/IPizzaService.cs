using PizzaStoreApp.Models;
using PizzaStoreApp.Models.ViewModel;

namespace PizzaStoreApp.Interfaces
{
    public interface IPizzaService
    {
        List<PizzaImageViewModel> GetAllPizzas();
    }
}
