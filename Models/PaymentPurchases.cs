using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class PaymentPurchases
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string user_id { get; set; }

        [BsonElement]
        public string date { get; set; }
        [BsonElement]
        public string Ref { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string purchase_id { get; set; }
        [BsonElement]

        public double montant { get; set; }
        [BsonElement]

        public double change { get; set; }
        [BsonElement]

        public string Reglement { get; set; }
        [BsonElement]

        public string notes { get; set; }
        [BsonElement]
        public string created_at { get; set; }
        [BsonElement]
        public string updated_at { get; set; }
        [BsonElement]
        public string deleted_at { get; set; }
    }
}