using AuraShop.Shared;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetAll;

public class GetAllCouponsQuery : IRequest<ServiceResult<List<CouponDto>>>;