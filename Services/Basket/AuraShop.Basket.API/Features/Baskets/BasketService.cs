using System.Text.Json;
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
            return json == null ? null : JsonSerializer.Deserialize<Data.Basket>(json);
        }

        public async Task SetBasketAsync(Guid userId, bool isAnonymous, Data.Basket basket, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId, isAnonymous);
            var json = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(key, json, cancellationToken);
        }

        public async Task RemoveBasketAsync(Guid userId, bool isAnonymous, CancellationToken cancellationToken = default)
        {
            var key = GetBasketKey(userId, isAnonymous);
            await distributedCache.RemoveAsync(key, cancellationToken);
        }

        public async Task<bool> RemoveBasketItemAsync(Guid userId, bool isAnonymous, Guid productId, CancellationToken cancellationToken = default)
        {
            var basket = await GetBasketAsync(userId, isAnonymous, cancellationToken);
            if (basket == null) return false;

            var item = basket.BasketItems.Find(x => x.ProductId == productId);
            if (item == null) return false;

            basket.BasketItems.Remove(item);

            if (basket.BasketItems.Count == 0)
                await RemoveBasketAsync(userId, isAnonymous, cancellationToken);
            else
                await SetBasketAsync(userId, isAnonymous, basket, cancellationToken);

            return true;
        }

        public async Task<bool> ApplyDiscountAsync(Guid userId, bool isAnonymous, string couponCode, decimal discountRate, CancellationToken cancellationToken = default)
        {
            var basket = await GetBasketAsync(userId, isAnonymous, cancellationToken);
            if (basket == null) return false;

            basket.ApplyDiscount(couponCode, discountRate);

            await SetBasketAsync(userId, isAnonymous, basket, cancellationToken);
            return true;
        }

    }
}
