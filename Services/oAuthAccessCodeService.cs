using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Swagger.Services
{
    public class oAuthAccessCodeService
    {
        private readonly IMongoCollection<oAuthAccessCodes> _collection;

        public oAuthAccessCodeService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<oAuthAccessCodes>("oAuthAccessCodes");
        }

        public List<oAuthAccessCodes> GetoAuthAccessCodes() => _collection.Find(oAuthAccessCodes => true).ToList();

        public oAuthAccessCodes GetoAuthAccessCode(string id) => _collection.Find(oAuthAccessCodes => oAuthAccessCodes.id == id).FirstOrDefault();

        public oAuthAccessCodes PostoAuthAccessCode(oAuthAccessCodes oauthAccessTokens)
        {
            _collection.InsertOne(oauthAccessTokens);
            return oauthAccessTokens;
        }

        public oAuthAccessCodes PutoAuthAccessCode(string id, oAuthAccessCodes oauthAccessTokens)
        {
            _collection.ReplaceOne(oauthAccessTokens => oauthAccessTokens.id == id, oauthAccessTokens);
            return oauthAccessTokens;
        }

        public oAuthAccessCodes DeleteoAuthAccessCode(string id)
        {
            var oauthAccessTokens = _collection.Find(oauthAccessTokens => oauthAccessTokens.id == id).FirstOrDefault();
            _collection.DeleteOne(oAuthAccessCodes => oAuthAccessCodes.id == id);
            return oauthAccessTokens;
        }

    }
}