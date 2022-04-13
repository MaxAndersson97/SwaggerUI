using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Swagger.Services
{
    public class AdjustmentDetailsService
    {
        private readonly IMongoCollection<AdjustmentDetails> _collection;

        public AdjustmentDetailsService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<AdjustmentDetails>("AdjustmentDetails");
        }

        public List<AdjustmentDetails> GetAdjustmentDetails() => _collection.Find(AdjustmentDetails => true).ToList();

        public AdjustmentDetails GetAdjustmentDetail(string id) => _collection.Find(AdjustmentDetails => AdjustmentDetails.id == id).FirstOrDefault();

        public JsonResult PostAdjustmentDetails(List<AdjustmentDetails> adjustmentDetails)
        {
            _collection.InsertMany(adjustmentDetails);
            JObject json = new();
            json.Add("statut", 200);
            json.Add("message", "Adjustment created successfully");
            json.Add("data", JArray.FromObject(adjustmentDetails));
            return new JsonResult(json);
        }

        public AdjustmentDetails PutAdjustmentDetails(string id, AdjustmentDetails adjustmentDetails)
        {
            _collection.ReplaceOne(adjustmentDetails => adjustmentDetails.id == id, adjustmentDetails);
            return adjustmentDetails;
        }

        public AdjustmentDetails DeleteAdjustmentDetails(string id)
        {
            var adjustmentDetails = _collection.Find(adjustmentDetails => adjustmentDetails.id == id).FirstOrDefault();
            _collection.DeleteOne(AdjustmentDetails => AdjustmentDetails.id == id);
            return adjustmentDetails;
        }

    }
}