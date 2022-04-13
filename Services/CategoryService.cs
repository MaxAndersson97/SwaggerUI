using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Swagger.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Categories> _collection;

        public CategoryService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Categories>("Categories");
        }

        public List<Categories> GetCategories() => _collection.Find(Categories => true).ToList();

        public Categories GetCategory(string id) => _collection.Find(Categories => Categories.id == id).FirstOrDefault();

        public Categories PostCategory(Categories categories)
        {
            categories.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.InsertOne(categories);
            return categories;
        }

        public Categories PutCategory(Categories categories)
        {
            categories.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.ReplaceOne(categorie => categorie.id == categories.id, categories);
            return categories;
        }

        public Categories DeleteCategory(string id)
        {
            var categorie = _collection.Find(Categorie => Categorie.id == id).FirstOrDefault();
            _collection.DeleteOne(Categorie => Categorie.id == id);
            return categorie;
        }
        public bool DeleteMultipleCategory(string[] ids)
        {
            var result = _collection.DeleteMany(Categorie => ids.Contains(Categorie.id));
            return result.DeletedCount > 0;
        }
    }
}