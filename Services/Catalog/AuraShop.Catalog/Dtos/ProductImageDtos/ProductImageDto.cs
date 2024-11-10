using AuraShop.Catalog.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Catalog.Dtos.ProductImageDtos
{
    public class ProductImageDto
    {
        public string Id { get; set; }
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
