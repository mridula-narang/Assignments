using AutoMapper;
using EventBookingApp.Models;
using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
        }
    }
}
