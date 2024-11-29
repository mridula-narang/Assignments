using HotelBookingApp.Interfaces;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            return Ok(rooms);
        }

        [HttpGet("{id}/GetRoomById")]
        [Authorize]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var rooms = await _roomService.GetRoom(id);
            return Ok(rooms);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoom(RoomDTO roomDTO)
        {
            var room = await _roomService.AddRoom(roomDTO);
            return Ok(room);
        }

        //delete a room
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomService.DeleteRoom(id);
            return Ok(room);
        }
    }
}
