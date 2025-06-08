using Asp.Versioning.Builder;
using AuraShop.Discount.Features.Coupons.Create;
using AuraShop.Discount.Features.Coupons.GetAll;

namespace AuraShop.Discount.Features.Coupons
{
    public static class CouponEndpointExt
    {
        public static void AddDiscountEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("Discount")
                .CreateDiscount()
                .GetAllCoupons()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
