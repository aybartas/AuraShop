using AuraShop.Basket.API.Dtos;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.GetBasket;

public record GetBasketQuery() : IRequest<ServiceResult<BasketDto>>;