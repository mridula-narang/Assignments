using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using AutoMapper;

namespace InsuraceClaimApp.Mappers
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
