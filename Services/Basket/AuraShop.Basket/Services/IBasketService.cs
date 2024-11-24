using AuraShop.Basket.Dtos;

namespace AuraShop.Basket.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(string userId);
        Task DeleteBasket(string userId);
        Task SaveBasket(BasketDto basket);
    }
}
