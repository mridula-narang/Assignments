using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAll();
        Task<BookingDTO> Get(int key);
        Task<BookingDTO> Add(BookingDTO entity);
    }
}
