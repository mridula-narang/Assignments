using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using AutoMapper;

namespace EFWebApiApp.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
