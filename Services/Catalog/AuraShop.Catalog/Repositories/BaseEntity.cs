using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Catalog.Repositories
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
