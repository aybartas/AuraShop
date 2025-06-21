namespace AuraShop.Catalog.Features.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Images { get; set; }
        public List<ProductColor> Colors { get; set; }
        public List<string> Sizes { get; set; }
    }
}
