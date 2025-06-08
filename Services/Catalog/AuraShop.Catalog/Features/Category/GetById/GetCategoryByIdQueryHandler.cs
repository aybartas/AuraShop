using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetById;

public class GetCategoryByIdQueryHandler(ICategoryService categoryService, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await  categoryService.GetCategoryByIdAsync(request.Id);

        if (category == null)
            return ServiceResult<CategoryDto>.Error("Category not found", $"Category with id {request.Id} not found", System.Net.HttpStatusCode.NotFound);
            
        var categoryDto = mapper.Map<CategoryDto>(category);

        return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
    }
}