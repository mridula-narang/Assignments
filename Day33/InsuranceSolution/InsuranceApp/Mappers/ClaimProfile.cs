using AutoMapper;
using InsuranceApp.Models;
using InsuranceApp.Models.DTOs;

namespace InsuranceApp.Mappers
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<ClaimRequestDTO, InsuranceClaim>();
            CreateMap<InsuranceClaim, ClaimRequestDTO>();
        }
    }
}
