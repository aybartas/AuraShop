using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Create
{
    public record CreateProductCommand(string Name , decimal Price , string Category , Guid CategoryId) : IRequest<ServiceResult<CreateProductCommandResponse>>;
}
