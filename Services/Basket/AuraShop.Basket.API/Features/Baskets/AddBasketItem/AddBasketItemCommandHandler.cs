using System.Text.Json;
using AuraShop.Basket.Data;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    private readonly IIdentityService _identityService;
    private readonly BasketService _basketService;

    public AddBasketItemCommandHandler( IIdentityService identityService, BasketService basketService)
    {
        _identityService = identityService;
        _basketService = basketService;
    }

    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        var existingBasket = await _basketService.GetBasketAsync(cancellationToken);

        var newItem = new BasketItem
        {
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            ImageUrl = request.ImageUrl,
            Quantity = request.Quantity,
            Price = request.Price,
        };

        if (string.IsNullOrEmpty(existingBasket))
        {
            var newBasket = new Data.Basket
            {
                UserId = _identityService.UserId,
                BasketItems = [newItem]
            };

            await _basketService.SetBasketAsync(newBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(existingBasket) ?? new Data.Basket
        {
            UserId = _identityService.UserId,
            BasketItems = []
        };

        var existingItem = currentBasket.BasketItems
            .FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem is not null)
            existingItem.Quantity += request.Quantity;
        else
            currentBasket.BasketItems.Add(newItem);

        if (currentBasket.HasDiscount)
            currentBasket.ReApplyDiscount();

        await _basketService.SetBasketAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}