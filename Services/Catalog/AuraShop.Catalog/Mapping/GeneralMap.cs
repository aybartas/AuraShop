using AuraShop.Catalog.Features.Product;
using AutoMapper;

namespace AuraShop.Catalog.Mapping
{
    public class GeneralMap : Profile
    {
        public GeneralMap()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
