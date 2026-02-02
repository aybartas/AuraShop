using AuraShop.Shared;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetByCode;

public class GetByCodeQuery : IRequest<ServiceResult<List<CouponDto>>>
{
    public string Code { get; set; }
}