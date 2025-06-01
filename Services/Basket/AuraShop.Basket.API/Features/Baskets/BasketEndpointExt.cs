using Asp.Versioning.Builder;
using AuraShop.Basket.API.Features.Baskets.AddBasketItem;
using AuraShop.Basket.API.Features.Baskets.ApplyDiscount;
using AuraShop.Basket.API.Features.Baskets.DeleteBasketItem;
using AuraShop.Basket.API.Features.Baskets.GetBasket;
using AuraShop.Basket.API.Features.Baskets.RemoveDiscount;

namespace AuraShop.Basket.API.Features.Baskets
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
