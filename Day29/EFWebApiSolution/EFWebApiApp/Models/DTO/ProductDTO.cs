namespace EFWebApiApp.Models.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public float PricePerUnit { get; set; } //changed from price to price per unit while learning testing
        public int Quantity { get; set; }
        public string BasicImage { get; set; } = string.Empty; //added while learning testing
    }
}
