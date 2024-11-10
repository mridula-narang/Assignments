using AutoMapper;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateDTO>();
            CreateMap<UserCreateDTO, User>();
        }
    }
}
