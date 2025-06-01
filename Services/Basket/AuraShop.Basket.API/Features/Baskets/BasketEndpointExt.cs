using Asp.Versioning.Builder;
using AuraShop.Basket.Features.Baskets.AddBasketItem;
using AuraShop.Basket.Features.Baskets.ApplyDiscount;
using AuraShop.Basket.Features.Baskets.DeleteBasketItem;
using AuraShop.Basket.Features.Baskets.GetBasket;
using AuraShop.Basket.Features.Baskets.RemoveDiscount;

namespace AuraShop.Basket.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Basket")
                .AddBasketItemGroupEndpoint()
                .DeleteBasketItemGroupEndpoint()
                .GetBasketItemGroupEndpoint()
                .ApplyCouponToBasketEndpoint()
                .RemoveDiscountGroupEndpoint()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
