using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _NetCore.DTO
{
    public class FilterData
    {
        [BsonElement]
        public string codeProduct { get; set; }

        [BsonElement]
        public string productName { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string categoryId { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public string brandId { get; set; }

        public Pagination pagination { get; set; }  
       
    }
}
