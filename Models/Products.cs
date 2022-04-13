using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        public string code { get; set; }

        [BsonElement]
        public string Type_barcode { get; set; }
        [BsonElement]
        public string name { get; set; }
        [BsonElement]

        public double cost { get; set; }
        [BsonElement]

        public double price { get; set; }
        [BsonElement]

        [BsonRepresentation(BsonType.ObjectId)]
        public string category_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string brand_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string unit_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string unit_sale_id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]

        public string unit_purchase_id { get; set; }
        [BsonElement]

        public double? TaxNet { get; set; }
        [BsonElement]

        public string tax_method { get; set; }
        [BsonElement]

        public List<product_images>? product_Images { get; set; }
        [BsonElement]

        public string note { get; set; }
        [BsonElement]

        public double? stock_alert { get; set; }
        [BsonElement]

        public string is_variant { get; set; }
        [BsonElement]

        public string is_active { get; set; }
        [BsonElement]
        public string created_at { get; set; }
        [BsonElement]
        public string updated_at { get; set; }
        [BsonElement]
        public string deleted_at { get; set; }
    }

    public class product_images
    {
        public int id { get; set; }
        public string image { get; set; }
        public bool isMain { get; set; }
    }


    public class UpdateImages
    {
        public string id { get; set; }
        public List<product_images> product_Images { get; set; }

    }
}