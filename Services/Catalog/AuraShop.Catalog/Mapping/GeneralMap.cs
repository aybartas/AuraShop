using AuraShop.Catalog.Dtos.CategoryDtos;
using AuraShop.Catalog.Dtos.ProductDetailDtos;
using AuraShop.Catalog.Dtos.ProductDtos;
using AuraShop.Catalog.Dtos.ProductImageDtos;
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

            CreateMap<ProductDetail, ProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, CreateProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Category, UpdateProductDto>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductImage, UpdateProductDto>().ReverseMap();
            CreateMap<ProductImage, CreateProductDto>().ReverseMap();
        }
    }
}
