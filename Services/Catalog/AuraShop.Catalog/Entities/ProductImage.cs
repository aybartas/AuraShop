﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuraShop.Catalog.Entities;

public class ProductImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public List<string> Images { get; set; }

    public string ProductId { get; set; }

    [BsonIgnore]
    public Product Product { get; set; }
}