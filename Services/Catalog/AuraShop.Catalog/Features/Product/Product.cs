﻿using AuraShop.Catalog.Database;

namespace AuraShop.Catalog.Features.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductColor> Colors { get; set; }
        public List<string> Sizes { get; set; }
        public List<string>? Images { get; set; }
    }
}
