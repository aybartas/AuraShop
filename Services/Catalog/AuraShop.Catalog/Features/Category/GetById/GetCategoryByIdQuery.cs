using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetById;

public class GetCategoryByIdQuery : IRequest<ServiceResult<CategoryDto>>
{
    public Guid Id { get; init; }
}