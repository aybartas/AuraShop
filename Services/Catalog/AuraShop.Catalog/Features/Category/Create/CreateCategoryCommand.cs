using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Category.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
}
