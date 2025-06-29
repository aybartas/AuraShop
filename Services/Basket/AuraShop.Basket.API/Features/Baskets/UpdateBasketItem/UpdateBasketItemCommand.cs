using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.UpdateBasketItem;

public class UpdateBasketItemCommand : IRequest<ServiceResult>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}