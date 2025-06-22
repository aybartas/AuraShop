using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.RemoveDiscount;

public class RemoveDiscountCommandHandler(BasketService basketService, IBasketAuthService basketAuthService) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCommand command, CancellationToken cancellationToken)
    {
        var userContext = basketAuthService.GetUser();

        var currentBasket = await basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        currentBasket.RemoveDiscount();

        await basketService.SetBasketAsync(userContext.UserId, userContext.IsAnonymous, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}