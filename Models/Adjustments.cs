using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Swagger.Models
{
    public class Adjustments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string user_id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> adjustment_detail_ids { get; set; } = new List<string>();

        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime date { get; set; }

        [BsonElement]
        public string products { get; set; }

        [BsonElement]
        public string Ref { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string warehouse_id { get; set; }
        [BsonElement]
        public double items { get; set; }
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