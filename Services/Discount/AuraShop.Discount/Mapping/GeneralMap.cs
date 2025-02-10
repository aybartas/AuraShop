
using AuraShop.Discount.Dtos.Coupons;
using AuraShop.Discount.Entities;
using AutoMapper;

namespace AuraShop.Catalog.Mapping
{
    public class GeneralMap : Profile
    {
        public GeneralMap()
        {
            CreateMap<Coupon, CreateDiscountCouponDto>().ReverseMap();
            CreateMap<Coupon, UpdateDiscountCouponDto>().ReverseMap();
            CreateMap<Coupon, DiscountCouponDto>().ReverseMap();
        }
    }
}
