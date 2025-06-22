using System.Net;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Create;

public class CreateCouponCommandHandler(ICouponService couponService, IMapper mapper, IIdentityService identityService) : IRequestHandler<CreateCouponCommand, ServiceResult<CreateCouponResponse>>
{
    public async Task<ServiceResult<CreateCouponResponse>> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {
        var userCoupons=  await couponService.GetUserCoupons(identityService.UserId.Value);

        var existingCoupon = userCoupons.FirstOrDefault(x => x.Code == command.Code);

        if (existingCoupon != null)
            return ServiceResult<CreateCouponResponse>.Error("Coupon code is already taken", $"Coupon code {command.Code} already exists", HttpStatusCode.BadRequest);

        var coupon = mapper.Map<Coupon>(command);

        coupon.Id = NewId.NextSequentialGuid();

        await couponService.CreateCouponAsync(coupon);

        return ServiceResult<CreateCouponResponse>.SuccessAsCreated(new CreateCouponResponse(coupon.Id),$"/api/categories/{coupon.Id}");
    }
}