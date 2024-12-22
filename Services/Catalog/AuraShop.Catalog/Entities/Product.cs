using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public Details Details { get; set; }
        public List<string> Images { get; set; }
        public string DetailId { get; set; }
        public string BrandId { get; set; }

        [BsonIgnore]
        public Brand Brand { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
