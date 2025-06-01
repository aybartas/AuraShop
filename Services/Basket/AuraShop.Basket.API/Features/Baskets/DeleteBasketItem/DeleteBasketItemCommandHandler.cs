using System.Text.Json;
using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandHandler(BasketService basketService): IRequestHandler<DeleteBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
    {
        var existingBasketJson = await basketService.GetBasketAsync(cancellationToken);

        if (existingBasketJson is null)
            return ServiceResult.ErrorAsNotFound("Basket not found");

        var currentBasket = JsonSerializer.Deserialize<Data.Basket>(existingBasketJson);

        var existingItem = currentBasket?.BasketItems.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existingItem is null)
            return ServiceResult.ErrorAsNotFound("Basket item not found");

        currentBasket.BasketItems.Remove(existingItem);

        if (currentBasket.BasketItems.Count == 0)
            await basketService.RemoveBasketAsync(cancellationToken);
        else
            await basketService.SetBasketAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}