using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Interfaces
{
    public interface IRoomService
    {
        Task<Room> AddRoom(RoomDTO roomDTO);
        Task<Room> DeleteRoom(int id);
        Task<Room> GetRoom(int id);
        Task<Room> UpdateRoom(int id, RoomDTO roomDTO);
        Task<IEnumerable<Room>> GetAllRooms();

    }
}
