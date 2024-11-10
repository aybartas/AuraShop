using AuraShop.Catalog.Dtos.ProductDetailDtos;
using AuraShop.Catalog.Dtos.ProductImageDtos;
using AuraShop.Catalog.Services.ProductImageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productImageService.GetAllCProductImagesAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var value = await _productImageService.GetProductImageByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductImageDto productImageDto)
        {
            await _productImageService.CreateProductImageAsync(productImageDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
             await _productImageService.DeleteProductImageAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductImageDto productImageDto)
        {
            await _productImageService.UpdateProductImageAsync(productImageDto);

            return Ok();
        }
    }
}
