using EFWebApiApp.Models;

namespace EFWebApiApp.Interfaces
{
    public interface IProductImageService 
    {
        Task AddImagesToProduct(int productId, List<string> imageUrls);
        Task<IEnumerable<ProductImage>> GetImagesByProductId(int productId);
    }
}
