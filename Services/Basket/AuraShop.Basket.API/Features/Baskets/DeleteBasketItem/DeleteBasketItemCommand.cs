using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid ProductId) : IRequest<ServiceResult>;