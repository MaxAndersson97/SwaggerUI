using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

namespace Swagger.Services
{
    public class PurchaseReturnService
    {
        private readonly IMongoCollection<PurchaseReturn> _collection;
        private readonly IMongoCollection<PurchaseReturnDetails> _collectionPurchaseReturnDetails;

        public PurchaseReturnService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");
            _collectionPurchaseReturnDetails = database.GetCollection<PurchaseReturnDetails>("PurchaseReturnDetails");
            _collection = database.GetCollection<PurchaseReturn>("PurchaseReturn");
        }

        public List<PurchaseReturn> GetPurchaseReturns() => _collection.Find(PurchaseReturn => true).ToList();
        public List<PurchaseReturn> GetWareHousePurchaseReturn(string id) => _collection.Find(PurchaseReturn => PurchaseReturn.warehouse_id == id).ToList();

        public JsonResult GetPurchaseReturn(string id)
        {

            PurchaseReturn purchaseReturns = _collection.Find(purchaseReturn => purchaseReturn.id == id).FirstOrDefault();

            if (purchaseReturns == null)
            {
                return new JsonResult(new { message = "Invalid Purchase Return Id", statut = 404 });
            }
            else
            {
              /*  JArray json = new();
                foreach (var item in purchaseReturns.purchase_return_detail_id)
                {
                    PurchaseReturnDetails purchaseReturnDetails = _collectionPurchaseReturnDetails.Find(purchaseReturnDetail => purchaseReturnDetail.id == item).FirstOrDefault();
                    json.Add(JObject.FromObject(purchaseReturnDetails));
                }
                JObject jsonObject = new();
                jsonObject.Add("id", purchaseReturns.id);
                jsonObject.Add("user_id", purchaseReturns.user_id);
                jsonObject.Add("date", purchaseReturns.date);
                jsonObject.Add("Ref", purchaseReturns.Ref);
                jsonObject.Add("provider_id", purchaseReturns.provider_id);
                jsonObject.Add("warehouse_id", purchaseReturns.warehouse_id);
                jsonObject.Add("tax_rate", purchaseReturns.tax_rate);
                jsonObject.Add("TaxNet", purchaseReturns.TaxNet);
                jsonObject.Add("discount", purchaseReturns.discount);
                jsonObject.Add("shopping", purchaseReturns.shopping);
                jsonObject.Add("GrandTotal", purchaseReturns.GrandTotal);
                jsonObject.Add("paid_amount", purchaseReturns.paid_amount);
                jsonObject.Add("statut", purchaseReturns.statut);
                jsonObject.Add("payment_status", purchaseReturns.payment_status);
                jsonObject.Add("notes", purchaseReturns.notes);
                jsonObject.Add("purchase_return_detail", json);*/


                return new JsonResult(new { statut = 200, data = purchaseReturns });
            }
        }

        public JsonResult PostPurchaseReturn(PurchaseReturn json)
        {
           /* PurchaseReturn purchaseReturn = new();
            purchaseReturn.user_id = json["user_id"].ToString();
            purchaseReturn.date = json["date"].ToString();
            purchaseReturn.Ref = json["Ref"].ToString();
            purchaseReturn.provider_id = json["provider_id"].ToString();
            purchaseReturn.warehouse_id = json["warehouse_id"].ToString();
            purchaseReturn.tax_rate = double.Parse(json["tax_rate"].ToString());
            purchaseReturn.TaxNet = double.Parse(json["TaxNet"].ToString());
            purchaseReturn.discount = double.Parse(json["discount"].ToString());
            purchaseReturn.shopping = double.Parse(json["shopping"].ToString());
            purchaseReturn.GrandTotal = double.Parse(json["GrandTotal"].ToString());
            purchaseReturn.paid_amount = double.Parse(json["paid_amount"].ToString());
            purchaseReturn.statut = json["statut"].ToString();
            purchaseReturn.payment_status = json["payment_status"].ToString();
            purchaseReturn.notes = json["notes"].ToString();
            purchaseReturn.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<PurchaseReturnDetails> purchaseReturnDetails = json["purchaseReturnDetails"].ToObject<List<PurchaseReturnDetails>>();
            _collectionPurchaseReturnDetails.InsertMany(purchaseReturnDetails);
            purchaseReturn.purchase_return_detail_id = purchaseReturnDetails.Select(x => x.id).ToList();*/
            _collection.InsertOne(json);

            return new JsonResult(new
            {
                statut = 200,
                message = "Purchase Return Created Successfully"
            });



        }
        public List<PurchaseReturn> GetPurchaseReturnsByFilter(string date, string reference, string supplier, string returnId, string paymentChoice)
        {
            if (date != null && reference != null && supplier != null && returnId != null && paymentChoice != null)
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.date == date && PurchaseReturn.Ref == reference && PurchaseReturn.provider_id == supplier && PurchaseReturn.id == returnId && PurchaseReturn.payment_status == paymentChoice).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.Ref == reference).ToList();
            }
            else if (supplier != null && supplier != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.provider_id == supplier).ToList();
            }
            else if (returnId != null && returnId != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.id == returnId).ToList();
            }
            else if (paymentChoice != null && paymentChoice != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.payment_status == paymentChoice).ToList();
            }
            else
            {
                return _collection.Find(PurchaseReturn => true).ToList();
            }
        }
        public List<PurchaseReturn> GetPurchaseReturnsByFilter(string date, string reference, string supplier, string warehouse_id, string statut, string paymentStatus)
        {
            if (date != null && reference != null && supplier != null && warehouse_id != null && paymentStatus != null && statut != null)
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.date == date && PurchaseReturn.Ref == reference && PurchaseReturn.provider_id == supplier && PurchaseReturn.id == warehouse_id && PurchaseReturn.statut == statut && PurchaseReturn.payment_status == paymentStatus).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.Ref == reference).ToList();
            }
            else if (supplier != null && supplier != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.provider_id == supplier).ToList();
            }
            else if (warehouse_id != null && warehouse_id != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.warehouse_id == warehouse_id).ToList();
            }
            else if (paymentStatus != null && paymentStatus != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.payment_status == paymentStatus).ToList();
            }
            else if (statut != null && statut != "")
            {
                return _collection.Find(PurchaseReturn => PurchaseReturn.statut == statut).ToList();
            }
            else
            {
                return _collection.Find(PurchaseReturn => true).ToList();
            }
        }
        public JsonResult PutPurchaseReturn(string id, PurchaseReturn json)
        {
            PurchaseReturn purchaseReturn = _collection.Find(purchaseReturn => purchaseReturn.id == id).FirstOrDefault();
            if (purchaseReturn == null)
            {
                return new JsonResult(new { message = "Invalid Adjustment Id", statut = 404 });
            }
            else
            {
               /* purchaseReturn.user_id = json["user_id"].ToString();
                purchaseReturn.date = json["date"].ToString();
                purchaseReturn.Ref = json["Ref"].ToString();
                purchaseReturn.provider_id = json["provider_id"].ToString();
                purchaseReturn.warehouse_id = json["warehouse_id"].ToString();
                purchaseReturn.tax_rate = double.Parse(json["tax_rate"].ToString());
                purchaseReturn.TaxNet = double.Parse(json["TaxNet"].ToString());
                purchaseReturn.discount = double.Parse(json["discount"].ToString());
                purchaseReturn.shopping = double.Parse(json["shopping"].ToString());
                purchaseReturn.GrandTotal = double.Parse(json["GrandTotal"].ToString());
                purchaseReturn.paid_amount = double.Parse(json["paid_amount"].ToString());
                purchaseReturn.statut = json["statut"].ToString();
                purchaseReturn.payment_status = json["payment_status"].ToString();
                purchaseReturn.notes = json["notes"].ToString();
                purchaseReturn.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                List<PurchaseReturnDetails> purchaseReturnDetails = json["adjustment_detail"].ToObject<List<PurchaseReturnDetails>>();
                foreach (var item in purchaseReturnDetails)
                {
                    if (_collectionPurchaseReturnDetails.Find(purchaseReturn => purchaseReturn.id == item.id).FirstOrDefault() != null)
                    {
                        _collectionPurchaseReturnDetails.ReplaceOne(purchaseReturn => purchaseReturn.id == item.id, item);
                    }
                    else
                    {
                        _collectionPurchaseReturnDetails.InsertOne(item);
                    }
                }

                purchaseReturn.purchase_return_detail_id = purchaseReturnDetails.Select(x => x.id).ToList();*/
                _collection.ReplaceOne(Adjustments => Adjustments.id == id, json);
                return new JsonResult(new { message = "Update Successfully", statut = 200 });
            }
        }

        public PurchaseReturn DeletePurchaseReturn(string id)
        {
            var PurchaseReturn = _collection.Find(PurchaseReturn => PurchaseReturn.id == id).FirstOrDefault();
            _collection.DeleteOne(PurchaseReturn => PurchaseReturn.id == id);
            return PurchaseReturn;
        }
        public bool DeleteMultiplePurchaseReturn(string[] ids)
        {
            var result = _collection.DeleteMany(Purchases => ids.Contains(Purchases.id));
            return result.DeletedCount > 0;
        }

    }
}