using static HotelBookingApp.Models.Booking;

namespace HotelBookingApp.Models.DTOs
{
    public class BookingDTO
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public BookingStatus Status { get; set; }
    }
}
