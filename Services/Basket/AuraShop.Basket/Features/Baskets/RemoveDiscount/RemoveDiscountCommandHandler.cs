using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;
using System.Text.Json;

namespace AuraShop.Basket.Features.Baskets.RemoveDiscount
{
    public class RemoveDiscountCommandHandler(BasketService basketService, IIdentityService identityService) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCommand command, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId.Value;

            var currentBasket = await basketService.GetBasketAsync(userId, cancellationToken);

            if (currentBasket is null)
                return ServiceResult.ErrorAsNotFound("Basket not found");

            currentBasket.RemoveDiscount();

            await basketService.SetBasketAsync(userId, currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}