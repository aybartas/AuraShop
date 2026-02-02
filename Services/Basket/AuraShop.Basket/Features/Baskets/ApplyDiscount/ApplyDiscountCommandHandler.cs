using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount
{
    public class ApplyDiscountCommandHandler(BasketService basketService, IDiscountService discountService, IIdentityService identityService) : IRequestHandler<ApplyCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyCouponCommand command, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId.Value;

            var currentBasket = await basketService.GetBasketAsync(userId, cancellationToken);

            if (currentBasket is null)
                return ServiceResult.ErrorAsNotFound("Basket not found");

            var validationResponse = await discountService.ValidateCouponAsync(command.CouponCode);

            if (!validationResponse.IsValid)
                return ServiceResult.BadRequest(validationResponse.ErrorMessage);

            currentBasket.ApplyDiscount(validationResponse.CouponCode, validationResponse.DiscountRate);

            await basketService.SetBasketAsync(userId, currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}