using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Discount.Database
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
