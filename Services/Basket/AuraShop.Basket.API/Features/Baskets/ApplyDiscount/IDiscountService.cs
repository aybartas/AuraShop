using System.Net.Http.Headers;
using System.Text.Json;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount
{
    public interface IDiscountService
    {
        Task<ValidationResponse> ValidateCouponAsync(string couponCode);
    }

    public class DiscountService(HttpClient client, IHttpContextAccessor httpContextAccessor)  : IDiscountService
    {
        public async Task<ValidationResponse> ValidateCouponAsync(string couponCode)
        {
            AddBearerToken();

            var response = await client.GetAsync($"/api/v1/discounts/coupons/{couponCode}/validate");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ValidationResponse>(content , new JsonSerializerOptions(){PropertyNameCaseInsensitive = true});

            return result;
        }

        private void AddBearerToken()
        {
            var header = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            var token = header.Replace("Bearer", "").Trim();

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }

}
