using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetAll;

public class GetAllProductQuery : IRequest<ServiceResult<List<ProductDto>>>;