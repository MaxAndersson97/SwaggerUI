
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Swagger.Models
{
    public class AdjustmentDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_variant { get; set; }
        [BsonElement]

        public double quantity { get; set; }
        [BsonElement]

        public string type { get; set; }
        [BsonElement]
        public string created_at { get; }
        [BsonElement]
        public string updated_at { get; }
    }
}