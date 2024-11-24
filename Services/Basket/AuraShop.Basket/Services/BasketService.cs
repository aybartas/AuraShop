using AuraShop.Basket.Dtos;
using AuraShop.Basket.Settings;

namespace AuraShop.Basket.Services;

public class BasketService : IBasketService
{
    private readonly IRedisService _redisService;
    public BasketService(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<BasketDto> GetBasket(string userId)
    {
        var existing = await _redisService.GetAsync<BasketDto>(userId);

        return existing;

    }

    public async Task DeleteBasket(string userId)
    {
        await _redisService.RemoveAsync(userId);
    }

    public async Task SaveBasket(BasketDto basket)
    {
        await _redisService.SetAsync(basket.UserId, basket);
    }
}