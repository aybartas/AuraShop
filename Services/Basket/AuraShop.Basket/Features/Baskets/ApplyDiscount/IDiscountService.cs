using System.Text.Json;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount
{
    public interface IDiscountService
    {
        Task<ValidationResponse> ValidateCouponAsync(string couponCode);
    }

    public class DiscountService(HttpClient client) : IDiscountService
    {
        public async Task<ValidationResponse> ValidateCouponAsync(string couponCode)
        {
            var response = await client.GetAsync($"/api/v1/discounts/coupons/{couponCode}/validate");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ValidationResponse>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result;
        }
    }
}
