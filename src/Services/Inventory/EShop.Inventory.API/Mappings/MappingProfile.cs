using AutoMapper;
using EShop.Inventory.API.Dto;
using EShop.Inventory.API.Entities;

namespace EShop.Inventory.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
