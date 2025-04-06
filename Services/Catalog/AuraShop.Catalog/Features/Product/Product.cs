using AuraShop.Catalog.Repositories;

namespace AuraShop.Catalog.Features.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public List<string>? Images { get; set; }
    }
}
