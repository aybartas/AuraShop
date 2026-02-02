using AuraShop.Shared;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Validate;

public class ValidateCouponQuery : IRequest<ServiceResult<ValidateCouponQueryResponse>>
{
    public string Code { get; set; }
}