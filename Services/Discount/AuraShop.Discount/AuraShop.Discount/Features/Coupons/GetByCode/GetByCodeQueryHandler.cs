using AuraShop.Shared;
using AutoMapper;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.GetByCode;

public class GetByCodeQueryHandler(ICouponService couponService, IMapper mapper) : IRequestHandler<GetByCodeQuery, ServiceResult<List<CouponDto>>>
{
    public async Task<ServiceResult<List<CouponDto>>> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
    {
        var categories = await couponService.GetAllCategoriesAsync();

        var mappedCategories = mapper.Map<List<CouponDto>>(categories);

        return ServiceResult<List<CouponDto>>.SuccessAsOk(mappedCategories);
    }
}