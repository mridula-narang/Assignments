using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HotelBookingApp.Models.Booking;

namespace HotelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingService.GetBookings();
            return Ok(bookings);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddBooking(BookingDTO bookingDTO)
        {
            var booking = await _bookingService.AddBooking(bookingDTO);
            return Ok(booking);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUserId(userId);
            return Ok(bookings);
        }

<<<<<<< HEAD
        [HttpPost("{userId}")]
=======
        [HttpPost]
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
        [Authorize]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, BookingStatus status)
        {
            var booking = await _bookingService.UpdateBookingStatus(bookingId, status);
            return Ok(booking);
        }
<<<<<<< HEAD
=======


        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> UpdateBookingStatus(int bookingId, BookingStatus status)
        //{
        //    var booking = await _bookingService.UpdateBookingStatus(bookingId, status);
        //    return Ok(booking);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateBooking(BookingDTO bookingDTO)
        //{
        //    var booking = await _bookingService.UpdateBooking(bookingDTO);
        //    return Ok(booking);
        //}
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
    }
}

