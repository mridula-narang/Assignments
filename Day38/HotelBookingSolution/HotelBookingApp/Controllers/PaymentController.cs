using HotelBookingApp.Interfaces;
using HotelBookingApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _paymentService.MakePayment(paymentDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing the payment.");
                return StatusCode(500, "An error occurred while processing the payment. Please try again later.");
            }
        }

        [HttpGet("ViewAllTransactions")]
        public async Task<IActionResult> ViewAllTransactions()
        {
            try
            {
                var transactions = await _paymentService.ViewAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving transactions.");
                return StatusCode(500, "An error occurred while retrieving transactions. Please try again later.");
            }
        }
    }
}
