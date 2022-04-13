using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Swagger.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Clients> _collection;

        public ClientService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");
            _collection = database.GetCollection<Clients>("Clients");
        }

        public List<Clients> GetClients() => _collection.Find(Clients => true).ToList();

        public Clients GetClient(string id) => _collection.Find(Clients => Clients.id == id).FirstOrDefault();
        public Clients LoginClient(string email, string password) => _collection.Find(Clients => Clients.email == email && Clients.code == int.Parse(password)).FirstOrDefault();

        public Clients PostClient(Clients Client)
        {

            Client.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.InsertOne(Client);
            return Client;
        }

        public Clients PutClient(string id, Clients Client)
        {

            Client.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Client != null && Client.code == 0)
            {
                var code = _collection.Find(c => true).ToList()?.Select(s => s.code)?.Max();
                Client.code = (code.GetValueOrDefault() == 0 ? 100 : code.Value) + 1;
            }
            _collection.ReplaceOne(Client => Client.id == id, Client);
            return Client;
        }

        public Clients DeleteClient(string id)
        {
            var Client = _collection.Find(Client => Client.id == id).FirstOrDefault();
            _collection.DeleteOne(Clients => Clients.id == id);
            return Client;
        }
        public bool DeleteMultipleUser(string[] ids)
        {
            var result = _collection.DeleteMany(Clients => ids.Contains(Clients.id));
            return result.DeletedCount > 0;
        }

        //public List<Products> GetFilterUsers(string code, string name, string phone, string email)
        //{
        //    if (code != null && code != null && name != null && phone != null && email != null)
        //    {
        //        return _collection.Find(Clients => Users.code == code && Products.name == product && Products.category_id == category && Products.brand_id == brand).ToList();
        //    }
        //    else if (code != null && code != "")
        //    {
        //        return _collection.Find(Products => Products.code == code).ToList();
        //    }
        //    else if (product != null && product != "")
        //    {
        //        return _collection.Find(Products => Products.name == product).ToList();
        //    }
        //    else if (category != null && category != "")
        //    {
        //        return _collection.Find(Products => Products.category_id == category).ToList();
        //    }
        //    else if (brand != null && brand != "")
        //    {
        //        return _collection.Find(Products => Products.brand_id == brand).ToList();
        //    }
        //    else
        //    {
        //        return _collection.Find(Products => true).ToList();
        //    }
        //}
    }
}