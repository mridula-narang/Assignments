using AutoMapper;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Mappers
{
    public class HotelProfile:Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelDTO, Hotel>();
            CreateMap<Hotel, HotelDTO>();
        }
    }
}
