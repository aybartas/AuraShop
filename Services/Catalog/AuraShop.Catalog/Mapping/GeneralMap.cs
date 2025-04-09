using AuraShop.Catalog.Features.Category;
using AuraShop.Catalog.Features.Category.Create;
using AuraShop.Catalog.Features.Product;
using AutoMapper;

namespace AuraShop.Catalog.Mapping
{
    public class GeneralMap : Profile
    {
        public GeneralMap()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        }
    }
}
