using System.Text.Json;
using AuraShop.Basket.API.Constants;
using AuraShop.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace AuraShop.Basket.API.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        public string CacheKey => string.Format(BasketConstants.BasketCacheKey, identityService.UserId);

        public async Task<string?> GetBasketAsync(CancellationToken cancellationToken = default)
        {
            var existingBasketJson = await distributedCache.GetStringAsync(CacheKey, token: cancellationToken);
            return existingBasketJson;
        }
        public async Task SetBasketAsync(Data.Basket basket,CancellationToken cancellationToken = default)
        {
            var updatedBasketJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(CacheKey, updatedBasketJson, token: cancellationToken);
        }
        public async Task RemoveBasketAsync(CancellationToken cancellationToken = default)
        {
            await distributedCache.RemoveAsync(CacheKey, token: cancellationToken);

        }
    }
}
