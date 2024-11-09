using AutoMapper;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Mappers
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();
        }
    }
}
