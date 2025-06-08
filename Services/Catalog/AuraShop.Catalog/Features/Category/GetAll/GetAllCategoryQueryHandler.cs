using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetAll;

public class GetAllCategoryQueryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
{
    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryService.GetAllCategoriesAsync();

        var mappedCategories = mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.SuccessAsOk(mappedCategories);
    }
}