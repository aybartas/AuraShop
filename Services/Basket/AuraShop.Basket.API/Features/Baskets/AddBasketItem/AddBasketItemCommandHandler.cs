using System.Text.Json;
using AuraShop.Basket.Data;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandHandler(BasketService basketService, IBasketAuthService basketAuthService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand command, CancellationToken cancellationToken)
    {
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

        // Get user ID and anon status from BasketAuthService
        var userContext = basketAuthService.GetUser();
        var userId = userContext.UserId;
        var isAnonymous = userContext.IsAnonymous;

        var existingBasketJson = await basketService.GetBasketAsync(userId, isAnonymous, cancellationToken);

        if (existingBasketJson is null)
        {
            var newBasket = new Data.Basket
            {
                BasketItems = [newItem]
            };

            await basketService.SetBasketAsync(userId, isAnonymous, newBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

   
        var existingItem = existingBasketJson.BasketItems.FirstOrDefault(x => x.ProductId == command.ProductId);

        if (existingItem != null)
            existingItem.Quantity += command.Quantity;
        else
            existingBasketJson.BasketItems.Add(newItem);


        await basketService.SetBasketAsync(userId, isAnonymous, existingBasketJson, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}