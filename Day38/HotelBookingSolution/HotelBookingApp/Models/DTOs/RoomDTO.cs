using static HotelBookingApp.Models.Room;

namespace HotelBookingApp.Models.DTOs
{
    public class RoomDTO
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
<<<<<<< HEAD
        public RoomStatus IsBooked { get; set; }
=======
        public bool IsBooked { get; set; }
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
        public string Features { get; set; } = string.Empty;
        public int Capacity { get; set; }

    }
}
