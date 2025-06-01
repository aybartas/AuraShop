using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.RemoveDiscount;

public class RemoveDiscountCommandHandler : IRequestHandler<RemoveDiscountCommand, ServiceResult>
{
    private readonly BasketService _basketService;
    public RemoveDiscountCommandHandler(BasketService basketService)
    {
        _basketService = basketService;
    }
    public async Task<ServiceResult> Handle(RemoveDiscountCommand command, CancellationToken cancellationToken)
    {
        var existingBasketJson = await _basketService.GetBasketAsync(cancellationToken);

        if (string.IsNullOrEmpty(existingBasketJson))
            return ServiceResult.ErrorAsNotFound("Basket not found");
        
        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(existingBasketJson);

        currentBasket.RemoveDiscount();

        await _basketService.SetBasketAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}