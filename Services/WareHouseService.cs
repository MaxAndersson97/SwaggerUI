using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Swagger.Services
{
    public class WareHouseService
    {
        private readonly IMongoCollection<WareHouses> _collection;

        public WareHouseService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<WareHouses>("WareHouses");
        }

        public List<WareHouses> GetWareHouses() => _collection.Find(WareHouses => true).ToList();

        public WareHouses GetWareHouse(string id) => _collection.Find(WareHouses => WareHouses.id == id).FirstOrDefault();

        public WareHouses PostWareHouse(WareHouses user)
        {
            _collection.InsertOne(user);
            return user;
        }

        public WareHouses PutWareHouse(string id, WareHouses user)
        {
            _collection.ReplaceOne(user => user.id == id, user);
            return user;
        }

        public WareHouses DeleteWareHouse(string id)
        {
            var user = _collection.Find(user => user.id == id).FirstOrDefault();
            _collection.DeleteOne(WareHouses => WareHouses.id == id);
            return user;
        }
        public bool DeleteMultipleWareHouse(string[] ids)
        {
            var result = _collection.DeleteMany(WareHouses => ids.Contains(WareHouses.id));
            return result.DeletedCount > 0;
        }

    }
}