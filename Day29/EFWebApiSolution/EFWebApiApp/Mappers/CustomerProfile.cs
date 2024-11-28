using AutoMapper;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;

namespace EFWebApiApp.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}
