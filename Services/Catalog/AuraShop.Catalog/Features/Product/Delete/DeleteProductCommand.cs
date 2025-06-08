using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Delete;

public record DeleteProductCommand(Guid Id) : IRequest<ServiceResult>;