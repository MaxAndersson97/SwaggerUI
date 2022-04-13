using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Swagger.Services
{
    public class UnitService
    {
        private readonly IMongoCollection<Units> _collection;

        public UnitService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Units>("Units");
        }

        public List<Units> GetUnits() => _collection.Find(Units => true).ToList();

        public Units GetUnit(string id) => _collection.Find(Units => Units.id == id).FirstOrDefault();

        public Units PostUnit(Units unit)
        {
            _collection.InsertOne(unit);
            return unit;
        }

        public Units PutUnit(string id, Units unit)
        {
            _collection.ReplaceOne(u => u.id == id, unit);
            return unit;
        }

        public Units DeleteUnit(string id)
        {
            var unit = _collection.Find(u => u.id == id).FirstOrDefault();
            _collection.DeleteOne(Units => Units.id == id);
            return unit;
        }
        public bool DeleteMultipleUnit(string[] ids)
        {
            var result = _collection.DeleteMany(Units => ids.Contains(Units.id));
            return result.DeletedCount > 0;
        }
    }
}