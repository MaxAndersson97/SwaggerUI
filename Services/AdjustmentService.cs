using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Swagger.Services
{
    public class AdjustmentService
    {
        private readonly IMongoCollection<Adjustments> _collection;
        private readonly IMongoCollection<AdjustmentDetails> _collectionAdjustment;

        public AdjustmentService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");
            _collectionAdjustment = database.GetCollection<AdjustmentDetails>("AdjustmentDetails");
            _collection = database.GetCollection<Adjustments>("Adjustments");
        }

        public List<Adjustments> GetAdjustments() => _collection.Find(Adjustments => true).ToList();
        
        //public List<Adjustments> GetFilterAdjustments(DateTime date, string reference, string warehouse) =>
        //_collection.Find(Adjustments => Adjustments.date == date && Adjustments.Ref == reference && Adjustments.warehouse_id == warehouse).ToList();
        public List<Adjustments> GetFilterAdjustments(DateTime? date, string reference, string warehouse)
        {
            if (date != null && reference != null && warehouse != null && warehouse != null)
            {
                _collection.Find(Adjustments => Adjustments.date == date && Adjustments.Ref == reference && Adjustments.warehouse_id == warehouse).ToList();
            }
            else if (date != null)
            {
                return _collection.Find(Adjustments => Adjustments.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Adjustments => Adjustments.Ref == reference).ToList();
            }
            else if (warehouse != null && warehouse != "")
            {
                return _collection.Find(Adjustments => Adjustments.warehouse_id == warehouse).ToList();
            }
           
           
                return _collection.Find(Adjustments => true).ToList();
           
        }
        public JsonResult GetAdjustment(string id)
        {

            Adjustments adjustment = _collection.Find(Adjustments => Adjustments.id == id).FirstOrDefault();

            if (adjustment == null)
            {
                return new JsonResult(new { message = "Invalid Adjustment Id", statut = 404 });
            }
            else
            {

                return new JsonResult(new { message = "Create Successfully", statut = 200, data = adjustment });
            }
        }

        public JsonResult PostAdjustments(Adjustments adjustments)
        {
            // Adjustments adjustment = new();
            // adjustment.user_id = json["user_id"].ToString();
            // adjustment.items = double.Parse(json["items"].ToString());
            // adjustment.notes = json["notes"].ToString();
            // adjustment.date = DateTime.Parse(json["date"].ToString());
            // adjustment.Ref = json["ref"].ToString();
            // adjustment.warehouse_id = json["warehouse_id"].ToString();
            // adjustment.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            // List<AdjustmentDetails> adjustmentDetails = json["adjustment_detail"].ToObject<List<AdjustmentDetails>>();
            // _collectionAdjustment.InsertMany(adjustmentDetails);
            // adjustment.adjustment_detail_ids = adjustmentDetails.Select(x => x.id).ToList();
            _collection.InsertOne(adjustments);

            return new JsonResult(new { message = "Create Successfully", statut = 201 });
        }

        public JsonResult PutAdjustments(string id, Adjustments json)
        {
            Adjustments adjustment = _collection.Find(Adjustments => Adjustments.id == id).FirstOrDefault();
            if (adjustment == null)
            {
                return new JsonResult(new { message = "Invalid Adjustment Id", statut = 404 });
            }
            else
            {
                // adjustment.user_id = json["user_id"].ToString();
                // adjustment.date = DateTime.Parse(json["date"].ToString());
                // adjustment.Ref = json["Ref"].ToString();
                // adjustment.warehouse_id = json["warehouse_id"].ToString();
                // adjustment.items = double.Parse(json["items"].ToString());
                // adjustment.notes = json["notes"].ToString();
                // adjustment.items = double.Parse(json["items"].ToString());
                // adjustment.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                // List<AdjustmentDetails> adjustmentDetail = json["adjustment_detail"].ToObject<List<AdjustmentDetails>>();
                // foreach (var item in adjustmentDetail)
                // {
                //     if (_collectionAdjustment.Find(AdjustmentDetails => AdjustmentDetails.id == item.id).FirstOrDefault() != null)
                //     {
                //         _collectionAdjustment.ReplaceOne(AdjustmentDetails => AdjustmentDetails.id == item.id, item);
                //     }
                //     else
                //     {
                //         _collectionAdjustment.InsertOne(item);
                //     }
                // }

                // adjustment.adjustment_detail_ids = adjustmentDetail.Select(x => x.id).ToList();
                _collection.ReplaceOne(Adjustments => Adjustments.id == id, json);
                return new JsonResult(new { message = "Update Successfully", statut = 200 });
            }
        }

        public Adjustments DeleteAdjustments(string id)
        {
            var adjustmentDetails = _collection.Find(adjustmentDetails => adjustmentDetails.id == id).FirstOrDefault();
            _collection.DeleteOne(Adjustments => Adjustments.id == id);
            return adjustmentDetails;
        }

        public bool DeleteMultipleAdjustments(string[] ids)
        {
            var result = _collection.DeleteMany(Adjustments => ids.Contains(Adjustments.id));
            return result.DeletedCount > 0;
        }

    }
}