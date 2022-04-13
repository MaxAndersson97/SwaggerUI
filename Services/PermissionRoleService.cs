using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Swagger.Services
{
    public class PermissionRoleService
    {
        private readonly IMongoCollection<PermissionRoles> _collection;

        public PermissionRoleService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<PermissionRoles>("PermissionRoles");
        }

        public List<PermissionRoles> GetPermissionRoles() => _collection.Find(PermissionRoles => true).ToList();

        public PermissionRoles GetPermissionRole(string id) => _collection.Find(PermissionRoles => PermissionRoles.id == id).FirstOrDefault();

        public PermissionRoles PostPermissionRole(PermissionRoles PermissionRole)
        {
            _collection.InsertOne(PermissionRole);
            return PermissionRole;
        }

        public PermissionRoles PutPermissionRole(string id, PermissionRoles PermissionRole)
        {
            _collection.ReplaceOne(PermissionRole => PermissionRole.id == id, PermissionRole);
            return PermissionRole;
        }

        public PermissionRoles DeletePermissionRole(string id)
        {
            var PermissionRole = _collection.Find(PermissionRole => PermissionRole.id == id).FirstOrDefault();
            _collection.DeleteOne(PermissionRole => PermissionRole.id == id);
            return PermissionRole;
        }

    }
}