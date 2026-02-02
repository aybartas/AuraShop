using AuraShop.Discount.Features.CouponUsages.Create;
using AutoMapper;

namespace AuraShop.Discount.Features.CouponUsages;

public class CouponUsageMap : Profile
{
    public CouponUsageMap()
    {
        CreateMap<CouponUsage, CreateCouponUsageCommand>().ReverseMap();
        CreateMap<CouponUsage, CouponUsageDto>().ReverseMap();
    }
}