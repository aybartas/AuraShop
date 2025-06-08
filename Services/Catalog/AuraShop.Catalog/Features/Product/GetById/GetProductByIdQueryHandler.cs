using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetById;

public class GetProductByIdQueryHandler(IProductService productService, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ServiceResult<ProductDto>>
{
    public async Task<ServiceResult<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(request.Id);

        if (product == null)
            return ServiceResult<ProductDto>.Error("Product not found", $"Product with id {request.Id} not found", System.Net.HttpStatusCode.NotFound);
            
        var productDto = mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto>.SuccessAsOk(productDto);
    }
}