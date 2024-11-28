using System.Text.Json.Serialization;

namespace HotelBookingApp.Models
{
    public class Room
    {
        public enum RoomType
        {
            Single,
            Double,
            Suite
        }

        public enum RoomStatus
        {
            Available,
            Booked
        }
        public int RoomId { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public RoomStatus IsBooked { get; set; }
        public string Name { get; set; }
        public string Features { get; set; } 
        public int Capacity { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore]
        public Hotel Hotel { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
