using System.Text.Json.Serialization;

namespace HotelApp.Models
{
    public class Booking
    {
        public enum BookingStatus
        {
            Pending,
            Confirmed,
            Cancelled
        }
        public int BookingId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User? Users { get; set; }
        public int HotelId { get; set; }

        [JsonIgnore]
        public Hotel? Hotel { get; set; }
        public int RoomId { get; set; }

        [JsonIgnore]
        public Room? Rooms { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        [JsonIgnore]
        public Payment? Payment { get; set; }
        [JsonIgnore]
        public CancellationPolicy? CancellationPolicy { get; set; }
        public BookingStatus Status { get; set; }
    }
}
