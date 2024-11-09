using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;

namespace EFWebApiApp.Interfaces
{
    public interface IProductService
    {
        //method to get all products
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<Product> AddProduct(ProductDTO productDto);
        //method to update only price of product
        Task<ProductDTO> UpdateProductPrice(int productId, double price);
        //method to get product by id
        Task<Product> GetProductById(int productId);
        //public Task<bool> AddNewProduct(ProductDTO product); //added while learning testing
    }
}
