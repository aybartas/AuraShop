using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;

namespace AuraShop.Discount.Features.CouponUsages.Create
{
    public record CreateCouponUsageCommand(Guid CouponId) : IRequest<ServiceResult<Coupons.Create.CreateCouponUsageResponse>>;

    public class CreateCouponUsageCommandValidator : AbstractValidator<CreateCouponUsageCommand>
    {
    }
    public record CreateCouponUsageResponse(Guid Id);

    public class CreateCouponUsageCommandHandler(ICouponService couponService, IIdentityService identityService, IMapper mapper) : IRequestHandler<CreateCouponUsageCommand, ServiceResult<Coupons.Create.CreateCouponUsageResponse>>
    {
        public async Task<ServiceResult<Coupons.Create.CreateCouponUsageResponse>> Handle(CreateCouponUsageCommand command, CancellationToken cancellationToken)
        {
            var couponUsage = mapper.Map<CouponUsage>(command);

            couponUsage.Id = NewId.NextSequentialGuid();
            couponUsage.AppliedAt = DateTime.UtcNow;
            couponUsage.UserId = identityService.UserId.Value;

            await couponService.CreateCouponUsageAsync(couponUsage);

            return ServiceResult<Coupons.Create.CreateCouponUsageResponse>.SuccessAsCreated(new Coupons.Create.CreateCouponUsageResponse(couponUsage.Id), $"/api/coupon-usages/{couponUsage.Id}");
        }
    }
}
