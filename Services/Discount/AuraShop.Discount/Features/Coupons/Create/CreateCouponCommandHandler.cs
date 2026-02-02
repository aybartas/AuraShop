using System.Net;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Create;

public class CreateCouponUsageCommandHandler(ICouponService couponService, IMapper mapper) : IRequestHandler<CreateCouponCommand, ServiceResult<CreateCouponUsageResponse>>
{
    public async Task<ServiceResult<CreateCouponUsageResponse>> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {

        var coupon = mapper.Map<Coupon>(command);

        coupon.Id = NewId.NextSequentialGuid();

        await couponService.CreateCouponAsync(coupon);

        return ServiceResult<CreateCouponUsageResponse>.SuccessAsCreated(new CreateCouponUsageResponse(coupon.Id),$"/api/discounts/{coupon.Id}");
    }
}