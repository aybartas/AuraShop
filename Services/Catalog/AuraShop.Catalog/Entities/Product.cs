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

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
    }
}
