using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Repositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        static List<Pizza> pizzas = new List<Pizza>()
        {
            new Pizza { Id = 1, Name = "Margherita", Price = 10.00f, Quantity = 10, Image = "https://cdn.shopify.com/s/files/1/0274/9503/9079/files/20220211142754-margherita-9920_5a73220e-4a1a-4d33-b38f-26e98e3cd986.jpg?v=1723650067", Description = "Tomato, Mozzarella, Basil" },
            new Pizza { Id = 2, Name = "Veg cheeze pizza", Price = 12.00f, Quantity = 10, Image = "https://i.ytimg.com/vi/AOnj_DBTZyI/maxresdefault.jpg", Description = "Mushroom,Tomato, Bell Peppers, Cheese"},

        };
        public async Task<Pizza> Add(Pizza entity)
        {
            if (pizzas.Count == 0)
            {
                entity.Id = 1;
            }
            else
                entity.Id = pizzas.Max(c => c.Id) + 1;
            pizzas.Add(entity);
            return entity;
        }

        public async Task<Pizza> Delete(int key)
        {
            var pizza = await Get(key);
            pizzas.Remove(pizza);
            return pizza;
        }

        public async Task<Pizza> Get(int key)
        {
            var pizza = pizzas.FirstOrDefault(c => c.Id == key);
            if (pizza == null)
            {
                throw new NoEntityFoundException("Pizza", key);
            }
            return pizza;
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            if (pizzas.Count == 0)
            {
                throw new CollectionEmptyException("Pizza");
            }
            return pizzas;
        }

        public async Task<Pizza> Update(Pizza entity)
        {
            var pizza = await Get(entity.Id);
            if (pizza == null)
            {
                throw new NoEntityFoundException("Pizza", entity.Id);
            }
            pizza.Name = entity.Name;
            pizza.Price = entity.Price;
            pizza.Quantity = entity.Quantity;
            pizza.Image = entity.Image;
            pizza.Description = entity.Description;
            return pizza;
        }
    }
}
