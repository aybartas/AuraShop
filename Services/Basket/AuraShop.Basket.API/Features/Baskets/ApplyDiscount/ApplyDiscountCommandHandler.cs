using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public class ApplyDiscountCommandHandler : IRequestHandler<ApplyCouponCommand, ServiceResult>
{
    private readonly BasketService _basketService;

    public ApplyDiscountCommandHandler(BasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<ServiceResult> Handle(ApplyCouponCommand command, CancellationToken cancellationToken)
    {
        var existingBasketJson = await _basketService.GetBasketAsync(cancellationToken);

        if (string.IsNullOrEmpty(existingBasketJson))
            return ServiceResult.ErrorAsNotFound("Basket not found");
        
        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(existingBasketJson);

        currentBasket.ApplyDiscount(command.Coupon,command.DiscountRate);

        await _basketService.SetBasketAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}