using AuraShop.Catalog.Dtos.ProductDetailDtos;
using AuraShop.Catalog.Services.ProductDetailsServices;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailsService _productDetailsService;
        public ProductDetailsController(IProductDetailsService productDetailsService)
        {
            _productDetailsService = productDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetailsList()
        {
            var values = await _productDetailsService.GetAllProductDetailsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailsById(string id)
        {
            var value = await _productDetailsService.GetProductDetailByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetails(CreateProductDetailDto categoryDto)
        {
            await _productDetailsService.CreateProductDetailAsync(categoryDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetails(string id)
        {
             await _productDetailsService.DeleteProductDetailAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetails(UpdateProductDetailDto productDetailDto)
        {
            await _productDetailsService.UpdateProductDetailAsync(productDetailDto);

            return Ok();
        }

    }
}
