using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyCouponCommand, ServiceResult>
{
    private readonly BasketService _basketService;
    private readonly IBasketAuthService _basketAuthService;

    public ApplyDiscountCommandHandler(BasketService basketService, IBasketAuthService basketAuthService)
    {
        _basketService = basketService;
        _basketAuthService = basketAuthService;
    }

    public async Task<ServiceResult> Handle(ApplyCouponCommand command, CancellationToken cancellationToken)
    {
        var userContext = _basketAuthService.GetUser();

        var currentBasket = await _basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        currentBasket.ApplyDiscount(command.Coupon, command.DiscountRate);

        await _basketService.SetBasketAsync(userContext.UserId, userContext.IsAnonymous, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}