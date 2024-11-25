using AutoMapper;
using InsuranceApp.Models.DTOs;
using InsuranceApp.Models;

namespace InsuranceApp.Mappers
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
