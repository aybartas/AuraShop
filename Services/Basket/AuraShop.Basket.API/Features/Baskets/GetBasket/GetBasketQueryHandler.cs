using AuraShop.Basket.Dtos;
using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.GetBasket;

public class GetBasketQueryHandler(IMapper mapper, BasketService basketService, IBasketAuthService basketAuthService)  : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var userContext = basketAuthService.GetUser();

        var currentBasket = await basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult<BasketDto>.ErrorAsNotFound("Basket not found");

        var basket = mapper.Map<BasketDto>(currentBasket);

        return ServiceResult<BasketDto>.SuccessAsOk(basket);
    }
}