using AuraShop.Basket.API.Data;
using AuraShop.Basket.API.Dtos;
using AutoMapper;

namespace AuraShop.Basket.API.Features.Baskets
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
