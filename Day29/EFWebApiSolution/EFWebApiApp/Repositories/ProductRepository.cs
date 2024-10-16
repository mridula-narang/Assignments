using EFWebApiApp.Contexts;
using EFWebApiApp.Exceptions;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFWebApiApp.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new CouldNotAddException("Product");
            }
        }

        public async Task<Product> Delete(int key)
        {
            var product = Get(key);
            if (product != null)
            {
                _context.Products.Remove(product.Result);
                await _context.SaveChangesAsync();
                return product.Result;
            }
            throw new NotFoundException("Product for delete");
        }

        public async Task<Product> Get(int key)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == key);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return products;
        }

        public async Task<Product> Update(int key, Product entity)
        {
            var product = await Get(key);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Quantity = entity.Quantity;
                product.Price = entity.Price;
                await _context.SaveChangesAsync();
                return product;
            }
            throw new NotFoundException("Product for update");
        }
    }
}
