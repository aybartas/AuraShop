using AuraShop.Catalog.Dtos.ProductDetailDtos;
using AuraShop.Catalog.Services.ProductDetailsServices;
using Microsoft.AspNetCore.Mvc;

namespace AuraShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductDetailsService _productService;

        public ProductsController(IProductDetailsService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetAllProductDetailsAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var value = await _productService.GetProductDetailByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDetailDto productDetailDto)
        {
            await _productService.CreateProductDetailAsync(productDetailDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
             await _productService.DeleteProductDetailAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDetailDto productDetailDto)
        {
            await _productService.UpdateProductDetailAsync(productDetailDto);

            return Ok();
        }
    }
}
