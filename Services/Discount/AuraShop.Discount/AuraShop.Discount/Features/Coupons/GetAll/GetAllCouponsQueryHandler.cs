using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetAll;

public class GetAllCouponsQueryHandler(ICouponService couponService, IMapper mapper) : IRequestHandler<GetAllCouponsQuery, ServiceResult<List<CouponDto>>>
{
    public async Task<ServiceResult<List<CouponDto>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
    {
        var categories = await couponService.GetAllCategoriesAsync();

        var mappedCategories = mapper.Map<List<CouponDto>>(categories);

        return ServiceResult<List<CouponDto>>.SuccessAsOk(mappedCategories);
    }
}