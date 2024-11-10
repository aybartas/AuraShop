using AuraShop.Catalog.Entities;

namespace AuraShop.Catalog.Dtos.ProductImageDtos;

public class CreateProductImageDto
{
    public List<string> Images { get; set; }
    public string ProductId { get; set; }
    public Product Product { get; set; }
}