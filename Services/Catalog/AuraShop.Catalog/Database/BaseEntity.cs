using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Catalog.Database
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
