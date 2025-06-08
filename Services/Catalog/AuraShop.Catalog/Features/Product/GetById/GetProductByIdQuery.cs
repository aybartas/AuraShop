using AuraShop.Catalog.Features.Category;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.GetById;

public class GetProductByIdQuery : IRequest<ServiceResult<ProductDto>>
{
    public Guid Id { get; init; }
}