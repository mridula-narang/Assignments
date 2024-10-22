using AutoMapper;
using EventBookingApp.Models;
using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Mappers
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
        }
    }
}
