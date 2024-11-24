using System.Security.Claims;
using AuraShop.Basket.Dtos;
using AuraShop.Basket.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketService _basketService;

        public BasketsController(IHttpContextAccessor httpContextAccessor, IBasketService basketService)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var basket = await _basketService.GetBasket(userId);

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBasket(BasketDto basketDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            basketDto.UserId = userId;

            await _basketService.SaveBasket(basketDto);

            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveBasket()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;

            await _basketService.DeleteBasket(userId);

            return Ok();
        }
    }
}
