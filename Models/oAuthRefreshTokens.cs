using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class oAuthRefreshTokens
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        public string access_token { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        [BsonElement]
        public string expires_at { get; set; }
    }
}