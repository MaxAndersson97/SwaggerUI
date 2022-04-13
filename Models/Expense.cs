using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        public string date { get; set; }

        [BsonElement]
        public string Ref { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string user_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string expense_category_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string warehouse_id { get; set; }
        [BsonElement]

        public string details { get; set; }
        [BsonElement]

        public double amount { get; set; }
        [BsonElement]
        public string created_at { get; set; }
        [BsonElement]
        public string updated_at { get; set; }
        [BsonElement]
        public string deleted_at { get; set; }
    }
}