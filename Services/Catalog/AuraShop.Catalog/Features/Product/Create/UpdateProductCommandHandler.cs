using System.Net;
using AuraShop.Catalog.Features.Category;
using AuraShop.Shared;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Create;

public class UpdateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResult<CreateProductCommandResponse>>
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(ICategoryService categoryService, IMapper mapper, IProductService productService)
    {
        _categoryService = categoryService;
        _mapper = mapper;
        _productService = productService;
    }
    public async Task<ServiceResult<CreateProductCommandResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var hasCategory =  await _categoryService.GetCategoryByIdAsync(command.CategoryId);

        if (hasCategory == null)
            return ServiceResult<CreateProductCommandResponse>.Error("Category not found", $"Category with id {command.CategoryId} not found", HttpStatusCode.BadRequest);

        var product = _mapper.Map<Product>(command);

        product.Id = NewId.NextSequentialGuid();

        await _productService.CreateProductAsync(product);

        return ServiceResult<CreateProductCommandResponse>.SuccessAsCreated(new CreateProductCommandResponse(product.Id),$"/api/products/{product.Id}");
    }
}