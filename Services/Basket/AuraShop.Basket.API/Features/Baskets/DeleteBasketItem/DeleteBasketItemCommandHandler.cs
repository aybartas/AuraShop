using System.Text.Json;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(BasketService basketService, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId.Value;

        var currentBasket = await basketService.GetBasketAsync(userId, cancellationToken);

        if (currentBasket is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        var existingItem = currentBasket?.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem is null)
            return ServiceResult.ErrorAsNotFound("Basket item not found");

        currentBasket.BasketItems.Remove(existingItem);

        if (currentBasket.BasketItems.Count == 0)
            await basketService.RemoveBasketAsync(userId, cancellationToken);
        else
            await basketService.SetBasketAsync(userId, currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}