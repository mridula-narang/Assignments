using AutoMapper;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;

namespace EFWebApiApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<int, Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> AddProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var addedProduct = await _productRepository.Add(product);
            return addedProduct;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _productRepository.Get(productId);
            return _mapper.Map<Product>(product);

        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> UpdateProductPrice(int productId, double price)
        {
            var product = await _productRepository.Get(productId);
            if (product == null)
            {
                return null;
            }
            product.Price = (float)price;
            var updatedProduct = await _productRepository.Update(productId,product);
            return _mapper.Map<ProductDTO>(updatedProduct);
        }
    }
}
