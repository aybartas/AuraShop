using AuraShop.Catalog.Features.Product.Create;
using AutoMapper;

namespace AuraShop.Catalog.Features.Product
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}
