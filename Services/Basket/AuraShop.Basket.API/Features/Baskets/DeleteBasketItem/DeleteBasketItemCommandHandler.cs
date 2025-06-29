using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(BasketService basketService, IBasketAuthService basketAuthService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        // Get current user context (userId + isAnonymous)
        var userContext = basketAuthService.GetUser();

        var currentBasket = await basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        var existingItem = currentBasket?.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem is null)
            return ServiceResult.ErrorAsNotFound("Basket item not found");

        currentBasket.BasketItems.Remove(existingItem);

        if (currentBasket.BasketItems.Count == 0)
            await basketService.RemoveBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);
        else
            await basketService.SetBasketAsync(userContext.UserId, userContext.IsAnonymous, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}