using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AuraShop.Basket.Features.Baskets
{
    public class BasketService(IDistributedCache distributedCache)
    {
        private static string GetBasketKey(Guid userId) => $"basket:user:{userId}";

        public async Task<Data.Basket?> GetBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId);
            var json = await distributedCache.GetStringAsync(key, cancellationToken);
            return json == null ? null : JsonSerializer.Deserialize<Data.Basket>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task SetBasketAsync(Guid userId, Data.Basket basket, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId);
            var json = JsonSerializer.Serialize(basket, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            await distributedCache.SetStringAsync(key, json, cancellationToken);
        }

        public async Task RemoveBasketAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId);
            await distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
