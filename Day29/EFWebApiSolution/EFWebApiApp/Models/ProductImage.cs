using System.ComponentModel.DataAnnotations;

namespace EFWebApiApp.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
