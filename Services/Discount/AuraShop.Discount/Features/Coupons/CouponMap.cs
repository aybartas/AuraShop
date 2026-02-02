
using AuraShop.Discount.Features.Coupons.Create;
using AutoMapper;

namespace AuraShop.Discount.Features.Coupons
{
    public class CouponMap : Profile
    {
        public CouponMap()
        {
            CreateMap<Coupon, CreateCouponCommand>().ReverseMap();
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
