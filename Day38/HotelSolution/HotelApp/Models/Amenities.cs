namespace HotelApp.Models
{
    public class Amenities
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Charges { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
