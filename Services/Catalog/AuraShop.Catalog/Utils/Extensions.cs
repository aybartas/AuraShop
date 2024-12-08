using AuraShop.Catalog.Entities;
using AuraShop.Catalog.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuraShop.Catalog.Utils
{
    public static class Extensions
    {
        public static GetProductRequest GetProductFilterAndSortDefinition(this GetProductFilter filter)
        {
            var builder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>();

            var brands = filter.Brands is { Length: > 0 } ? filter.Brands.Split(",").ToList() : new List<string>();
            var categories = filter.Categories is { Length: > 0 } ? filter.Categories.Split(",").ToList() : new List<string>();

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                filters.Add(builder.Regex("Name", new BsonRegularExpression(filter.SearchText, "i")));
            }

            if (!string.IsNullOrEmpty(filter.ProductName))
            {
                filters.Add(builder.Regex("Name", new BsonRegularExpression(filter.ProductName, "i")));
            }

            if (brands.Count > 0)
            {
                filters.Add(builder.In("Brand", brands));
            }

            if (categories.Count > 0)
            {
                filters.Add(builder.In("CategoryId", categories));
            }

            if (filter.MinPrice.HasValue)
            {
                filters.Add(builder.Gte("Price", filter.MinPrice.Value));
            }

            if (filter.MaxPrice.HasValue)
            {
                filters.Add(builder.Lte("Price", filter.MaxPrice.Value));
            }

            var filterDefinition = filters.Count > 0 ? builder.And(filters) : builder.Empty; 

            SortDefinition<Product> sortDefinition = null;
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                sortDefinition = filter.Ascending.HasValue && filter.Ascending.Value
                    ? Builders<Product>.Sort.Ascending(filter.SortBy)
                    : Builders<Product>.Sort.Descending(filter.SortBy);
            }

            var result = new GetProductRequest
            {
                SortDefinition = sortDefinition,
                FilterDefinition = filterDefinition
            };
            return result;
        }
    }
}
