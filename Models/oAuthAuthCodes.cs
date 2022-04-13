using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class oAuthAuthCodes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        public long user_id { get; set; }

        [BsonElement]
        public long client_id { get; set; }
        [BsonElement]
        public string scopes { get; set; }
        [BsonElement]

        public int revoked { get; set; }
        [BsonElement]
        public string expries_at { get; set; } = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
    }
}