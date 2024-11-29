using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Models;

namespace HotelBookingApp.Interfaces
{
    public interface IHotelService
    {
        public Task<Hotel> AddHotel(HotelDTO hotelDTO);
        public Task<Hotel> DeleteHotel(int id);
        public Task<Hotel> GetHotel(int id);
        public Task<Hotel> UpdateHotelCheckIn(int id, TimeSpan time);
        public Task<IEnumerable<Hotel>> GetAllHotel();
        public Task<IEnumerable<Room>> GetRoomsByHotelId(int hotelId);

    }
}
