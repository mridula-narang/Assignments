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
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ShoppingContext shoppingContext, ILogger<ProductRepository> logger)
        {
            _context = shoppingContext;
            _logger = logger;
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
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Product");
            }
        }

        public async Task<Product> Delete(int key)
        {
            try
            {
                var product = await Get(key);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return product;
                }
                throw new NotFoundException("Product for delete");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new NotFoundException("Product not found");
            }
        }

        public async Task<Product> Get(int key)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == key);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (products.Count == 0)
                {
                    throw new CollectionEmptyException("Products");
                }
                return products;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Product> Update(int key, Product entity)
        {
            try
            {
                var product = await Get(key);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.Quantity = entity.Quantity;
                    product.Price = entity.Price;
                    product.BasicImage = entity.BasicImage;
                    await _context.SaveChangesAsync();
                    return product;
                }
                throw new NotFoundException("Product for update");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
