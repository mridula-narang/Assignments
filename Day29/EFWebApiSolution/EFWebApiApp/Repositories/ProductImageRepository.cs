using EFWebApiApp.Contexts;
using EFWebApiApp.Exceptions;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EFWebApiApp.Repositories
{
    public class ProductImageRepository : IRepository<int, ProductImage>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<ProductImageRepository> _logger; 

        public ProductImageRepository(ShoppingContext context, ILogger<ProductImageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ProductImage> Add(ProductImage entity)
        {
            try
            {
                _context.ProductImages.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Product Image");
            }
        }

        public async Task<ProductImage> Delete(int key)
        {
            try
            {
                var productImage = await Get(key);
                if (productImage != null)
                {
                    _context.ProductImages.Remove(productImage);
                    await _context.SaveChangesAsync();
                    return productImage;
                }
                throw new NotFoundException("Product for delete");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new NotFoundException("Product not found");
            }
        }

        public async Task<ProductImage> Get(int key)
        {
            try
            {
                var productImage = await _context.ProductImages.FirstOrDefaultAsync(p => p.ImageId == key);
                if (productImage == null)
                {
                    throw new NotFoundException("Product Image");
                }
                return productImage;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            try
            {
                var productImages = await _context.ProductImages.ToListAsync();
                if (productImages.Count == 0)
                {
                    throw new CollectionEmptyException("Product Images");
                }
                return productImages;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<ProductImage> Update(int key, ProductImage entity)
        {
            try
            {
                var productImage = await Get(key);
                if (productImage != null)
                {
                    productImage.ImageUrl = entity.ImageUrl;
                    await _context.SaveChangesAsync();
                    return productImage;
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
