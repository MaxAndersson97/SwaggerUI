using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Swagger.Services
{
    public class CurrencieService
    {
        private readonly IMongoCollection<Currencies> _collection;

        public CurrencieService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Currencies>("Currencies");
        }

        public List<Currencies> GetCurrencies() => _collection.Find(Currencies => true).ToList();

        public Currencies GetCurrency(string id) => _collection.Find(Currencies => Currencies.id == id).FirstOrDefault();

        public Currencies PostCurrency(Currencies Currencies)
        {
            Currencies.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.InsertOne(Currencies);
            return Currencies;
        }

        public Currencies PutCurrency(string id, Currencies Currencies)
        {
            Currencies.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.ReplaceOne(Currencies => Currencies.id == id, Currencies);
            return Currencies;
        }

        public Currencies DeleteCurrency(string id)
        {
            var Currencies = _collection.Find(Currencies => Currencies.id == id).FirstOrDefault();
            _collection.DeleteOne(Currencies => Currencies.id == id);
            return Currencies;
        }

        public bool DeleteMultipleCurrency(string[] ids)
        {
            var result = _collection.DeleteMany(Currencies => ids.Contains(Currencies.id));
            return result.DeletedCount > 0;
        }

    }
}