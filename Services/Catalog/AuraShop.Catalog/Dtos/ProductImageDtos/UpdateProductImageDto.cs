using AuraShop.Catalog.Entities;

namespace AuraShop.Catalog.Dtos.ProductImageDtos
{
    public class UpdateProductImageDto
    {
        public string Id { get; set; }
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
 