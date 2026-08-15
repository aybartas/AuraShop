using System.Net;
using AuraShop.Catalog.Features.Category;
using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Update;

public class UpdateProductCommandHandler(
    ICategoryService categoryService,
    IMapper mapper,
    IProductService productService)
    : IRequestHandler<UpdateProductCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var hasProduct = await productService.GetProductByIdAsync(command.Id);

        if (hasProduct == null)
            return ServiceResult.ErrorAsNotFound();

        var hasCategory = await categoryService.GetCategoryByIdAsync(command.CategoryId);

        if (hasCategory == null)
            return ServiceResult<UpdateProductCommandResponse>.Error("Category not found", $"Category with id {command.CategoryId} not found", HttpStatusCode.BadRequest);

        var product = mapper.Map<Product>(command);

        await productService.UpdateProductAsync(product);

        return ServiceResult.SuccessAsNoContent();
    }
}