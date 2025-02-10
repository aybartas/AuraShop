using AuraShop.Discount.Dtos.Coupons;
using AuraShop.Discount.Entities;
using AuraShop.Discount.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Discount.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService , IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscountCoupon(int id ,[FromBody] UpdateDiscountCouponDto updateDiscountDto)
        {
            var value = await _discountService.GetDiscountCouponById(id);

            value.Rate = updateDiscountDto.Rate;
            value.ExpireDate = updateDiscountDto.ExpireDate;
            value.IsActive = updateDiscountDto.IsActive;
            value.Rate = updateDiscountDto.Rate;
            
            _discountService.UpdateDiscountCouponAsync(value);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createDiscountDto)
        {
            var coupon = _mapper.Map<Coupon>(createDiscountDto);
            await _discountService.CreateDiscountCouponAsync(coupon);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CreateDiscountCoupon(int id)
        {
            var value = await _discountService.GetDiscountCouponById(id);

            await _discountService.DeleteDiscountCouponAsync(value);
            return Ok();
        }
    }
}
