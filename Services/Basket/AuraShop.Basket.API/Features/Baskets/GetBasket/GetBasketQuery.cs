using AuraShop.Basket.Dtos;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.GetBasket;

public record GetBasketQuery() : IRequest<ServiceResult<BasketDto>>;