using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Swagger.Models
{
    public class PermissionRoles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string permission_id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string role_id { get; set; }
    }
}