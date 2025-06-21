using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetAllProduct;

public class GetAllProductsQueryResponse
{
    public List<ProductDto> Data { get; set; }
}

public class GetAllProductQueryHandler(IProductService productService, IMapper mapper) : IRequestHandler<GetAllProductQuery, ServiceResult<GetAllProductsQueryResponse>>
{
    public async Task<ServiceResult<GetAllProductsQueryResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productService.GetAllProductsAsync();

        var productDtos = mapper.Map<List<ProductDto>>(products);

        var response = new GetAllProductsQueryResponse
        {
            Data = productDtos
        };
        return ServiceResult<GetAllProductsQueryResponse>.SuccessAsOk(response);
    }
}