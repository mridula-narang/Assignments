using HotelBookingApp.Misc;

namespace HotelBookingApp.Models
{
    public class Payment
    {
        public enum PaymentType
        {
            CreditCard,
            DebitCard,
            Cash
        }
        public int PaymentId { get; set; }
        [PaymentValidator]
        public PaymentType ModeOfPayment { get; set; }
        public string CardNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
