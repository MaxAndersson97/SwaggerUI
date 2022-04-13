using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class TransferDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }


        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string product_variant_id { get; set; }
        [BsonElement]

        public double cost { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string purchase_unit_id { get; set; }

        [BsonElement]
        public double TaxNet { get; set; }

        [BsonElement]
        public double discount { get; set; }

        [BsonElement]
        public double quantity { get; set; }

        [BsonElement]
        public double total { get; set; }

        [BsonElement]
        public string tax_method { get; set; }

        [BsonElement]
        public string discount_method { get; set; } = "1";
        [BsonElement]
        public string created_at { get; set; }
        [BsonElement]
        public string updated_at { get; set; }
        [BsonElement]
        public string deleted_at { get; set; }
    }
}