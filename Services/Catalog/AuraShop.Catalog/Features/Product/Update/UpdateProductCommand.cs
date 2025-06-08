using AuraShop.Catalog.Features.Product.Create;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Update
{
    public record UpdateProductCommand(Guid Id,string Name , decimal Price , string Category , Guid CategoryId) : IRequest<ServiceResult>;
}
