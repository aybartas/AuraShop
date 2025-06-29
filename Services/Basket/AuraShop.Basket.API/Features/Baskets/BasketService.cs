using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace AuraShop.Basket.Features.Baskets
{
    public class BasketService(IDistributedCache distributedCache)
    {
        private static string GetBasketKey(Guid userId, bool isAnonymous) => isAnonymous ? $"basket:anon:{userId}" : $"basket:user:{userId}";

        public async Task<Data.Basket?> GetBasketAsync(Guid userId, bool isAnonymous, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId, isAnonymous);
            var json = await distributedCache.GetStringAsync(key, cancellationToken);
            return json == null ? null : JsonSerializer.Deserialize<Data.Basket>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task SetBasketAsync(Guid userId, bool isAnonymous, Data.Basket basket, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId, isAnonymous);
            var json = JsonSerializer.Serialize(basket, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            await distributedCache.SetStringAsync(key, json, cancellationToken);
        }

        public async Task RemoveBasketAsync(Guid userId, bool isAnonymous, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId, isAnonymous);
            await distributedCache.RemoveAsync(key, cancellationToken);
        }
    }
}
