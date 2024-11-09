using AutoMapper;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;

namespace InsuraceClaimApp.Mappers
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
