using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Models;

namespace HotelBookingApp.Interfaces
{
    public interface IHotelService
    {
        public Task<Hotel> AddHotel(HotelDTO hotelDTO);
        public Task<Hotel> DeleteHotel(int id);
        public Task<Hotel> GetHotel(int id);
        public Task<Hotel> UpdateHotel(int id, HotelDTO hotelDTO);
        public Task<IEnumerable<Hotel>> GetAllHotel();
    }
}
