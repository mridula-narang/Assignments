using AutoMapper;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Models;

namespace HotelBookingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<int, Payment> _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IRepository<int, Payment> paymentRepository,
            IMapper mapper,
            ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaymentDTO> MakePayment(PaymentDTO paymentDTO)
        {
            try
            {
                // Map DTO to Payment model
                var paymentEntity = _mapper.Map<Payment>(paymentDTO);

                // Add the payment to the repository
                var addedPayment = await _paymentRepository.Add(paymentEntity);

                // Map the saved entity back to DTO
                return _mapper.Map<PaymentDTO>(addedPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process payment.");
                throw new Exception("Payment processing failed. Please try again later.");
            }
        }

        public async Task<IEnumerable<PaymentDTO>> ViewAllTransactions()
        {
            try
            {
                var payments = await _paymentRepository.GetAll();
                return payments.Select(payment => _mapper.Map<PaymentDTO>(payment)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve transactions.");
                throw new Exception("Failed to retrieve transactions. Please try again later.");
            }
        }

    }
}
