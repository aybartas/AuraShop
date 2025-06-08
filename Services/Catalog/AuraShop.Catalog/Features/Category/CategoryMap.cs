using AuraShop.Catalog.Features.Category.Create;
using AutoMapper;

namespace AuraShop.Catalog.Features.Category
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
