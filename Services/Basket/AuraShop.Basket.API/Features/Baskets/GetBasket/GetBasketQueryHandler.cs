﻿using System.Text.Json;
using AuraShop.Basket.Dtos;
using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.GetBasket;

public class GetBasketQueryHandler(IMapper mapper, BasketService basketService): IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var existingBasketJson = await basketService.GetBasketAsync(cancellationToken);

        if (existingBasketJson is null)
            return ServiceResult<BasketDto>.ErrorAsNotFound("Basket not found");

        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(existingBasketJson);

        var basket = mapper.Map<BasketDto>(currentBasket);

        return ServiceResult<BasketDto>.SuccessAsOk(basket);
    }
}