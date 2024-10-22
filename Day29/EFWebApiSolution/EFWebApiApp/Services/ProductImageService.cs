using EFWebApiApp.Exceptions;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using EFWebApiApp.Repositories;

namespace EFWebApiApp.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<int, ProductImage> _imageRepository;
        private readonly IRepository<int, Product> _productRepository;


        public ProductImageService(IRepository<int, ProductImage> imageRepository, IRepository<int, Product> productRepository)
        {
            _imageRepository = imageRepository;
            _productRepository = productRepository;
        }
        public async Task AddImagesToProduct(int productId, List<string> imageUrls)
        {
            var product = await _productRepository.Get(productId);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            foreach (var imageUrl in imageUrls)
            {
                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImageUrl = imageUrl
                };
                await _imageRepository.Add(productImage);
            }
        }

        public async Task<IEnumerable<Models.ProductImage>> GetImagesByProductId(int productId)
        {
            try
            {
                var images = await _imageRepository.GetAll();
                return images.Where(img => img.ProductId == productId);
            }
            catch (Exception e)
            {

                throw new CollectionEmptyException("Product Image");
            }
        }
    }
}
