using AutoMapper;
using InsuranceApp.Models;
using InsuranceApp.Models.DTOs;

namespace InsuranceApp.Mappers
{
    public class PolicyProfile : Profile
    {
        public PolicyProfile()
        {
            CreateMap<PolicyDTO, Policy>();
            CreateMap<Policy, PolicyDTO>();
        }
    }
}
