using AuraShop.Basket.Dtos;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IMapper mapper, BasketService basketService, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId.Value;

            var currentBasket = await basketService.GetBasketAsync(userId, cancellationToken);

            var basket = mapper.Map<BasketDto>(currentBasket);

            return ServiceResult<BasketDto>.SuccessAsOk(basket);
        }
    }
}