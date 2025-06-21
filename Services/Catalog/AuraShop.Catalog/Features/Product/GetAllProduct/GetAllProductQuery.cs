using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetAllProduct;

public class GetAllProductQuery : IRequest<ServiceResult<GetAllProductsQueryResponse>>;