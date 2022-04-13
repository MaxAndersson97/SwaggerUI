using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Swagger.Services
{
    public class ProvidersService
    {
        private readonly IMongoCollection<Providers> _collection;

        public ProvidersService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Providers>("Providers");
        }

        public List<Providers> GetProviders() => _collection.Find(Providers => true).ToList();

        public Providers GetProvider(string id) => _collection.Find(Providers => Providers.id == id).FirstOrDefault();

        public Providers PostProvider(Providers Providers)
        {
            _collection.InsertOne(Providers);
            return Providers;
        }

        public Providers PutProvider(string id, Providers Providers)
        {
            _collection.ReplaceOne(Providers => Providers.id == id, Providers);
            return Providers;
        }

        public Providers DeleteProvider(string id)
        {
            var Providers = _collection.Find(Providers => Providers.id == id).FirstOrDefault();
            _collection.DeleteOne(Providers => Providers.id == id);
            return Providers;
        }
        public bool DeleteMultipleSuplier(string[] ids)
        {
            var result = _collection.DeleteMany(Providers => ids.Contains(Providers.id));
            return result.DeletedCount > 0;
        }

    }
}