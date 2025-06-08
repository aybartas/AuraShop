using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetAll;

public class GetAllProductQueryHandler(IProductService productService, IMapper mapper) : IRequestHandler<GetAllProductQuery, ServiceResult<List<ProductDto>>>
{
    public async Task<ServiceResult<List<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productService.GetAllProductsAsync();

        var productDtos = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.SuccessAsOk(productDtos);
    }
}