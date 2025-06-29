using Asp.Versioning.Builder;
using AuraShop.Discount.Features.Coupons.Create;
using AuraShop.Discount.Features.Coupons.GetAll;
using AuraShop.Discount.Features.Coupons.Validate;
using AuraShop.Discount.Features.CouponUsages.Create;

namespace AuraShop.Discount.Features
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts/coupons").WithTags("Coupons")
                .CreateCoupon()
                .GetAllCoupons()
                .ValidateCoupon()
                .WithApiVersionSet(apiVersionSet);

            app.MapGroup("api/v{version:apiVersion}/discounts/coupon-usages").WithTags("CouponUsages")
                .CreateCouponUsage()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
