using System.Net;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MassTransit;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Create;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, ServiceResult<CreateCouponResponse>>
{
    private readonly ICouponService _couponService;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    public CreateCouponCommandHandler(ICouponService couponService, IMapper mapper, IIdentityService identityService)
    {
        _couponService = couponService;
        _mapper = mapper;
        _identityService = identityService;
    }
    public async Task<ServiceResult<CreateCouponResponse>> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {
        var userCoupons=  await _couponService.GetUserCoupons(_identityService.UserId.Value);

        var existingCoupon = userCoupons.FirstOrDefault(x => x.Code == command.Code);

        if (existingCoupon != null)
            return ServiceResult<CreateCouponResponse>.Error("Coupon code is already taken", $"Coupon code {command.Code} already exists", HttpStatusCode.BadRequest);

        var coupon = _mapper.Map<Coupon>(command);

        coupon.UserId = _identityService.UserId.Value;
        coupon.Id = NewId.NextSequentialGuid();

        await _couponService.CreateCouponAsync(coupon);

        return ServiceResult<CreateCouponResponse>.SuccessAsCreated(new CreateCouponResponse(coupon.Id),$"/api/categories/{coupon.Id}");
    }
}