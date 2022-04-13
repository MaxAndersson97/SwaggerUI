using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Swagger.Services
{
    public class BrandService
    {
        private readonly IMongoCollection<Brands> _collection;

        public BrandService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Brands>("Brands");
        }

        public List<Brands> GetBrands() => _collection.Find(Brands => true).ToList();

        public Brands GetBrand(string id) => _collection.Find(Brands => Brands.id == id).FirstOrDefault();

        public Brands PostBrand(Brands brands)
        {
            brands.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.InsertOne(brands);
            return brands;
        }

        public Brands PutBrand(string id, Brands brands)
        {
            brands.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.ReplaceOne(brands => brands.id == id, brands);
            return brands;
        }

        public Brands DeleteBrand(string id)
        {
            var brands = _collection.Find(brands => brands.id == id).FirstOrDefault();
            _collection.DeleteOne(Brands => Brands.id == id);
            return brands;
        }
        public bool DeleteMultipleBrand(string[] ids)
        {
            var result = _collection.DeleteMany(brands => ids.Contains(brands.id));
            return result.DeletedCount > 0;
        }
    }
}