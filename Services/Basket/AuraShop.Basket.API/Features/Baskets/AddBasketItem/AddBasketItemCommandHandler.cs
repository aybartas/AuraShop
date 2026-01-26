using System.Text.Json;
using AuraShop.Basket.Data;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(BasketService basketService, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand command, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId.Value;

        var newItem = new BasketItem
        {
            ProductId = command.ProductId,
            ProductName = command.ProductName,
            ImageUrl = command.ImageUrl,
            Quantity = command.Quantity,
            Price = command.Price,
            Size = command.Size,
            Color = command.Color,
        };

        var existingBasketJson = await basketService.GetBasketAsync(userId, cancellationToken);

        if (existingBasketJson is null)
        {
            var newBasket = new Data.Basket
            {
                BasketItems = [newItem]
            };

            await basketService.SetBasketAsync(userId, newBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
   
        var existingItem = existingBasketJson.BasketItems.FirstOrDefault(x => x.ProductId == command.ProductId);

        if (existingItem != null)
            existingItem.Quantity += command.Quantity;
        else
            existingBasketJson.BasketItems.Add(newItem);

        await basketService.SetBasketAsync(userId, existingBasketJson, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}