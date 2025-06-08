using AuraShop.Order.Application.Dtos;
using AuraShop.Order.Domain.Entities;
using AutoMapper;

namespace AuraShop.Order.Application.Mappings
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<Domain.Entities.Order, OrderDto>()
                .ForMember(dest => dest.TotalPriceBeforeDiscount,
                    opt => opt.MapFrom(src => src.TotalPriceBeforeDiscount))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.TotalPrice))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
        }
    }
}
