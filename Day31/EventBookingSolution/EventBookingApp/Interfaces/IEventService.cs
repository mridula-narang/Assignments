using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAll();
        Task<EventDTO> Get(int key);
        Task<EventDTO> Add(EventDTO entity);
    }
}
