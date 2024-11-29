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

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, BookingStatus status)
        {
            var booking = await _bookingService.UpdateBookingStatus(bookingId, status);
            return Ok(booking);
        }

        [HttpGet("available-rooms")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var availableRooms = await _bookingService.GetAvailableRoomsForBooking();
            return Ok(availableRooms);
        }

    }
}

