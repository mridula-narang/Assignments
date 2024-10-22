using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using AutoMapper;

namespace EFWebApiApp.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, Models.DTO.ProductDTO>()
                .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.Price));
            CreateMap<Models.DTO.ProductDTO, Models.Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PricePerUnit));
        }
    }
}
