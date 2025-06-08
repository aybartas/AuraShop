using System.Net;
using AuraShop.Catalog.Features.Category;
using AuraShop.Shared;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Create;

public class UpdateProductCommandHandler(
    ICategoryService categoryService,
    IMapper mapper,
    IProductService productService)
    : IRequestHandler<CreateProductCommand, ServiceResult<CreateProductCommandResponse>>
{
    public async Task<ServiceResult<CreateProductCommandResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var hasCategory =  await categoryService.GetCategoryByIdAsync(command.CategoryId);

        if (hasCategory == null)
            return ServiceResult<CreateProductCommandResponse>.Error("Category not found", $"Category with id {command.CategoryId} not found", HttpStatusCode.BadRequest);

        var product = mapper.Map<Product>(command);

        product.Id = NewId.NextSequentialGuid();

        await productService.CreateProductAsync(product);

        return ServiceResult<CreateProductCommandResponse>.SuccessAsCreated(new CreateProductCommandResponse(product.Id),$"/api/products/{product.Id}");
    }
}