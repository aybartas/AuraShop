using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid ProductId) : IRequest<ServiceResult>;