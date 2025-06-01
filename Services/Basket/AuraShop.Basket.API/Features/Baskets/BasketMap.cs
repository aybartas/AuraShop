using AuraShop.Basket.Data;
using AuraShop.Basket.Dtos;
using AutoMapper;

namespace AuraShop.Basket.Features.Baskets
{
    public class BasketMap : Profile
    {
        public BasketMap()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        }
    }
}
