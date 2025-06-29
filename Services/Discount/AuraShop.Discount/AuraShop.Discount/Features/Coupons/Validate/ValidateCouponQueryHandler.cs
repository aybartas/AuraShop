using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Validate;

public class ValidateCouponQueryHandler(ICouponService couponService, IIdentityService identityService) : IRequestHandler<ValidateCouponQuery, ServiceResult<ValidateCouponQueryResponse>>
{
    public async Task<ServiceResult<ValidateCouponQueryResponse>> Handle(ValidateCouponQuery request, CancellationToken cancellationToken)
    {

        
        var coupon = await couponService.GetCouponByCode(request.Code);

        var response = new ValidateCouponQueryResponse
        {
            IsValid = false,
            CouponCode = request.Code,
        };

        if (coupon == null)
        {
            response.ErrorMessage = "Coupon not found";
            return ServiceResult<ValidateCouponQueryResponse>.SuccessAsOk(response);
        }

        if (coupon.ExpireDate < DateTime.UtcNow)
        {
            response.ErrorMessage = "Coupon is expired";
            return ServiceResult<ValidateCouponQueryResponse>.SuccessAsOk(response);
        }

        var userCoupons = await couponService.GetCouponUsagesByUserIdAsync(identityService.UserId.Value);

        if (userCoupons.FirstOrDefault(x => x.CouponId == coupon.Id) != null)
        {
            response.ErrorMessage = "Coupon is already used";
            return ServiceResult<ValidateCouponQueryResponse>.SuccessAsOk(response);
        }

        return ServiceResult<ValidateCouponQueryResponse>.SuccessAsOk(new ValidateCouponQueryResponse
        {
            IsValid = true,
            DiscountRate = coupon.DiscountRate,
            CouponCode = coupon.Code
        });
    }
}