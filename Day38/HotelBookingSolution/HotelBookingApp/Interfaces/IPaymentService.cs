using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Interfaces
{
    public interface IPaymentService
    {
        public Task<PaymentDTO> MakePayment(PaymentDTO paymentDTO);
        public Task<IEnumerable<PaymentDTO>> ViewAllTransactions();
    }
}
