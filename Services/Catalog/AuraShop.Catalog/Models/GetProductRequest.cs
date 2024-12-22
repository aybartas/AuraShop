using AuraShop.Catalog.Entities;
using MongoDB.Driver;

namespace AuraShop.Catalog.Utils
{
    public class GetProductRequest
    {
        public SortDefinition<Product> SortDefinition { get; set; }
        public FilterDefinition<Product> FilterDefinition { get; set; }
    }
}
