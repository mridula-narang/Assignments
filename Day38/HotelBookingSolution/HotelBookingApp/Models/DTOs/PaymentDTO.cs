using HotelBookingApp.Misc;
using static HotelBookingApp.Models.Payment;

namespace HotelBookingApp.Models.DTOs
{
    [PaymentValidator]
    public class PaymentDTO
    {
        public PaymentType ModeOfPayment { get; set; } 
        public string CardNumber { get; set; } 
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int BookingId { get; set; }
    }
}
