namespace HotelBookingApp.Models
{
    public class CancellationPolicy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal CancellationFee { get; set; }
        public bool EligibleForRefund { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
