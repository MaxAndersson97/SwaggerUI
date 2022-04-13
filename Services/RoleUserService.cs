using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Swagger.Services
{
    public class RoleUserService
    {
        private readonly IMongoCollection<RoleUsers> _collection;

        public RoleUserService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<RoleUsers>("RoleUsers");
        }

        public List<RoleUsers> GetRoleUsers() => _collection.Find(RoleUser => true).ToList();

        public RoleUsers GetRoleUser(string id) => _collection.Find(RoleUser => RoleUser.id == id).FirstOrDefault();

        public RoleUsers PostRoleUser(RoleUsers user)
        {
            _collection.InsertOne(user);
            return user;
        }

        public RoleUsers PutRoleUser(string id, RoleUsers user)
        {
            _collection.ReplaceOne(user => user.id == id, user);
            return user;
        }

        public RoleUsers DeleteRoleUser(string id)
        {
            var user = _collection.Find(user => user.id == id).FirstOrDefault();
            _collection.DeleteOne(RoleUser => RoleUser.id == id);
            return user;
        }

    }
}