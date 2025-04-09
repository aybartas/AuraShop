using System.Net;
using AuraShop.Catalog.Services;
using AuraShop.Shared;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Catalog.Features.Category.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var existing=  await _categoryService.GetCategoryByNameAsync(command.Name);

        if (existing != null)
        {
            return ServiceResult<CreateCategoryResponse>.Error("Category name already taken", $"Category name {command.Name} already exists", HttpStatusCode.BadRequest);
        }

        var category = _mapper.Map<Category>(command);

        category.Id = NewId.NextSequentialGuid();

        await _categoryService.CreateCategoryAsync(category);

        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"");
    }
}