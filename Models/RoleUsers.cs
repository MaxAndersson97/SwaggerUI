using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class RoleUsers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string user_id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string role_id { get; set; }
        [BsonElement]
        public string created_at { get; set; }
        [BsonElement]
        public string updated_at { get; set; }
        [BsonElement]
        public string deleted_at { get; set; }
    }
}