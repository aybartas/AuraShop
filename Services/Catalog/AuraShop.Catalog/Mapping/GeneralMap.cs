using AuraShop.Catalog.Dtos.BrandDtos;
using AuraShop.Catalog.Dtos.CategoryDtos;
using AuraShop.Catalog.Dtos.ProductDtos;
using AuraShop.Catalog.Entities;
using AutoMapper;

namespace AuraShop.Catalog.Mapping
{
    public class GeneralMap : Profile
    {
        public GeneralMap()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Category, UpdateProductDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
        }
    }
}
