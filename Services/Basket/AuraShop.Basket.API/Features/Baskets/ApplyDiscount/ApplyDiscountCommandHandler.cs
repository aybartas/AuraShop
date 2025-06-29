using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public class ApplyDiscountCommandHandler(BasketService basketService, IDiscountService discountService , IBasketAuthService basketAuthService) : IRequestHandler<ApplyCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ApplyCouponCommand command, CancellationToken cancellationToken)
    {
        var userContext = basketAuthService.GetUser();

        var currentBasket = await basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        var validationResponse = await discountService.ValidateCouponAsync(command.CouponCode);

        if (!validationResponse.IsValid)
            return ServiceResult.BadRequest(validationResponse.ErrorMessage);
        

        currentBasket.ApplyDiscount(validationResponse.CouponCode,validationResponse.DiscountRate);

        await basketService.SetBasketAsync(userContext.UserId, userContext.IsAnonymous, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}