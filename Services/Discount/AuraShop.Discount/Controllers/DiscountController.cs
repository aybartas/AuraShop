using AuraShop.Discount.Dtos.Coupons;
using AuraShop.Discount.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscountCouponList()
        {
            var values = await _discountService.GetAllDiscountCouponsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            var values = await _discountService.GetDiscountCouponById(id);
            return Ok(values);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateDiscountDto)
        {
             await _discountService.UpdateDiscountCouponAsync(updateDiscountDto);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createDiscountDto)
        {
            await _discountService.CreateDiscountCouponAsync(createDiscountDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CreateDiscountCoupon(int id)
        {
            await _discountService.DeleteDiscountCouponAsync(id);
            return Ok();
        }
    }
}
