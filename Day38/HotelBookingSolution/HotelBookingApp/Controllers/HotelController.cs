using HotelBookingApp.Interfaces;
using HotelBookingApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelService.GetAllHotel();
            return Ok(hotels);
        }

        [HttpGet("{id}/GetHotelById")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotels = await _hotelService.GetHotel(id);
            return Ok(hotels);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddHotel(HotelDTO hotelDTO)
        {
            try
            {
                var result = await _hotelService.AddHotel(hotelDTO);
                return Ok("Hotel Added Successfully");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
