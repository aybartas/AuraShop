using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Category.GetAll;

public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;