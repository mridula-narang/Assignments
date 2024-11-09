using AutoMapper;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Mappers
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomDTO, Room>();
            CreateMap<Room, RoomDTO>();
        }
    }
}
