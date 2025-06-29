using AuraShop.Basket.Features.Baskets.DeleteBasketItem;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.UpdateBasketItem;

public class UpdateBasketItemCommandHandler(BasketService basketService, IBasketAuthService basketAuthService) : IRequestHandler<UpdateBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateBasketItemCommand request, CancellationToken cancellationToken)
    {
        var userContext = basketAuthService.GetUser();

        var currentBasket = await basketService.GetBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        var existingItem = currentBasket?.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem is null)
            return ServiceResult.ErrorAsNotFound("Basket item not found");

        existingItem.Quantity = request.Quantity;

        if (existingItem.Quantity <= 0)
            currentBasket.BasketItems.Remove(existingItem);

        if (currentBasket.BasketItems.Count == 0)
            await basketService.RemoveBasketAsync(userContext.UserId, userContext.IsAnonymous, cancellationToken);
        else
            await basketService.SetBasketAsync(userContext.UserId, userContext.IsAnonymous, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}