using System.Net;
using AuraShop.Catalog.Features.Category;
using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResult>
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

    public async Task<ServiceResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var hasProduct = await _productService.GetProductByIdAsync(command.Id);

        if (hasProduct == null)
            return ServiceResult.ErrorAsNotFound();

        var hasCategory = await _categoryService.GetCategoryByIdAsync(command.CategoryId);

        if (hasCategory == null)
            return ServiceResult<UpdateProductCommandResponse>.Error("Category not found", $"Category with id {command.CategoryId} not found", HttpStatusCode.BadRequest);

        var product = _mapper.Map<Product>(command);

        await _productService.CreateProductAsync(product);

        return ServiceResult.SuccessAsNoContent();
    }
}