using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.AddBasketItem;

public record AddBasketItemCommand(Guid ProductId, string ProductName, decimal Price, int Quantity, string ImageUrl) : IRequest<ServiceResult>;