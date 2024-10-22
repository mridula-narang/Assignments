using AutoMapper;
using EventBookingApp.Models;
using EventBookingApp.Models.DTOs;
namespace EventBookingApp.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
