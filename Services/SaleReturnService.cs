using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Swagger.Services
{
    public class SaleReturnService
    {
        private readonly IMongoCollection<SaleReturns> _collection;
        private readonly IMongoCollection<SaleReturnDetails> _collectionSaleReturnDetails;


        public SaleReturnService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<SaleReturns>("SaleReturns");
            _collectionSaleReturnDetails = database.GetCollection<SaleReturnDetails>("SaleReturnDetails");
        }

        public List<SaleReturns> GetSaleReturns() => _collection.Find(SaleReturns => true).ToList();
        public List<SaleReturns> GetWareHouseSaleReturns(string id) => _collection.Find(SaleReturns => SaleReturns.warehouse_id == id).ToList();

        public JsonResult GetSaleReturn(string id)
        {

            SaleReturns saleReturns = _collection.Find(SaleReturns => SaleReturns.id == id).FirstOrDefault();

            if (saleReturns == null)
            {
                return new JsonResult(new { message = "Invalid Adjustment Id", statut = 404 });
            }
            else
            {
              /*  JArray json = new();
                foreach (var item in saleReturns.sale_return_detail_id)
                {
                    SaleReturnDetails saleReturnDetails = _collectionSaleReturnDetails.Find(SaleReturnDetails => SaleReturnDetails.id == item).FirstOrDefault();
                    json.Add(JObject.FromObject(saleReturnDetails));
                }
                JObject jsonObject = new();
                jsonObject.Add("id", saleReturns.id);
                jsonObject.Add("user_id", saleReturns.user_id);
                jsonObject.Add("date", saleReturns.date);
                jsonObject.Add("ref", saleReturns.Ref);
                jsonObject.Add("client_id", saleReturns.client_id);
                jsonObject.Add("warehouse_id", saleReturns.warehouse_id);
                jsonObject.Add("tax_rate", saleReturns.tax_rate);
                jsonObject.Add("taxNet", saleReturns.TaxNet);
                jsonObject.Add("discount", saleReturns.discount);
                jsonObject.Add("shipping", saleReturns.shipping);
                jsonObject.Add("grandTotal", saleReturns.GrandTotal);
                jsonObject.Add("paid_amount", saleReturns.paid_amount);
                jsonObject.Add("payment_status", saleReturns.payment_status);
                jsonObject.Add("statut", saleReturns.statut);
                jsonObject.Add("notes", saleReturns.notes);
                jsonObject.Add("sale_return_details", json);
              */

                return new JsonResult(new { message = "Create Successfully", statut = 200, data = saleReturns });
            }
        }

        public JsonResult PostSaleReturn(SaleReturns json)
        {
          /*  SaleReturns saleReturn = new();
            saleReturn.user_id = json["user_id"].ToString();
            saleReturn.date = json["date"].ToString();
            saleReturn.Ref = json["ref"].ToString();
            saleReturn.client_id = json["client_id"].ToString();
            saleReturn.warehouse_id = json["warehouse_id"].ToString();
            saleReturn.tax_rate = double.Parse(json["tax_rate"].ToString());
            saleReturn.TaxNet = double.Parse(json["taxNet"].ToString());
            saleReturn.discount = double.Parse(json["discount"].ToString());
            saleReturn.shipping = double.Parse(json["shipping"].ToString());
            saleReturn.GrandTotal = double.Parse(json["grandTotal"].ToString());
            saleReturn.paid_amount = double.Parse(json["paid_amount"].ToString());
            saleReturn.payment_status = json["payment_status"].ToString();
            saleReturn.statut = json["statut"].ToString();
            saleReturn.notes = json["notes"].ToString();
            saleReturn.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<SaleReturnDetails> saleReturnDetails = json["sale_return_details"].ToObject<List<SaleReturnDetails>>();
            _collectionSaleReturnDetails.InsertMany(saleReturnDetails);
            saleReturn.sale_return_detail_id = saleReturnDetails.Select(x => x.id).ToList();*/
            _collection.InsertOne(json);

            return new JsonResult(new { message = "Create Successfully", statut = 201 });
        }

        public JsonResult PutSaleReturn(string id, SaleReturns json)
        {
            SaleReturns saleReturns = _collection.Find(SaleReturns => SaleReturns.id == id).FirstOrDefault();
            if (saleReturns == null)
            {
                return new JsonResult(new { message = "Invalid Adjustment Id", statut = 404 });
            }
            else
            {/*
                saleReturns.user_id = json["user_id"].ToString();
                saleReturns.user_id = json["user_id"].ToString();
                saleReturns.date = json["date"].ToString();
                saleReturns.Ref = json["ref"].ToString();
                saleReturns.client_id = json["client_id"].ToString();
                saleReturns.warehouse_id = json["warehouse_id"].ToString();
                saleReturns.tax_rate = double.Parse(json["tax_rate"].ToString());
                saleReturns.TaxNet = double.Parse(json["taxNet"].ToString());
                saleReturns.discount = double.Parse(json["discount"].ToString());
                saleReturns.shipping = double.Parse(json["shipping"].ToString());
                saleReturns.GrandTotal = double.Parse(json["grandTotal"].ToString());
                saleReturns.paid_amount = double.Parse(json["paid_amount"].ToString());
                saleReturns.payment_status = json["payment_status"].ToString();
                saleReturns.statut = json["statut"].ToString();
                saleReturns.notes = json["notes"].ToString();
                saleReturns.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                List<SaleReturnDetails> saleReturnDetails = json["sale_return_details"].ToObject<List<SaleReturnDetails>>();
                foreach (var item in saleReturnDetails)
                {
                    if (_collectionSaleReturnDetails.Find(SaleReturnDetails => SaleReturnDetails.id == item.id).FirstOrDefault() != null)
                    {
                        _collectionSaleReturnDetails.ReplaceOne(SaleReturnDetails => SaleReturnDetails.id == item.id, item);
                    }
                    else
                    {
                        _collectionSaleReturnDetails.InsertOne(item);
                    }
                }

                saleReturns.sale_return_detail_id = saleReturnDetails.Select(x => x.id).ToList();*/
                _collection.ReplaceOne(SaleReturns => SaleReturns.id == id, json);
                return new JsonResult(new { message = "Update Successfully", statut = 200 });
            }
        }

        public List<SaleReturns> GetSalesRetrurnByFilter(string date, string reference, string customer, string Return, string paymentChoice)
        {
            if (date != null && reference != null && customer != null && Return != null && paymentChoice != null)
            {
                return _collection.Find(Purchases => Purchases.date == date && Purchases.Ref == reference && Purchases.client_id == customer && Purchases.id == Return && Purchases.payment_status == paymentChoice).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Purchases => Purchases.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Purchases => Purchases.Ref == reference).ToList();
            }
            else if (customer != null && customer != "")
            {
                return _collection.Find(Purchases => Purchases.client_id == customer).ToList();
            }
            else if (Return != null && Return != "")
            {
                return _collection.Find(Purchases => Purchases.id == Return).ToList();
            }
            else if (paymentChoice != null && paymentChoice != "")
            {
                return _collection.Find(Purchases => Purchases.payment_status == paymentChoice).ToList();
            }
            else
            {
                return _collection.Find(Purchases => true).ToList();
            }
        }
        public List<SaleReturns> GetSalesRetrurnByFilter(string date, string reference, string customer, string warehouse_id, string statut, string paymentStatus)
        {
            if (date != null && reference != null && customer != null && warehouse_id != null && paymentStatus != null && statut != null)
            {
                return _collection.Find(Purchases => Purchases.date == date && Purchases.Ref == reference && Purchases.client_id == customer && Purchases.warehouse_id == warehouse_id && Purchases.payment_status == paymentStatus && Purchases.statut == statut).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Purchases => Purchases.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Purchases => Purchases.Ref == reference).ToList();
            }
            else if (customer != null && customer != "")
            {
                return _collection.Find(Purchases => Purchases.client_id == customer).ToList();
            }
            else if (warehouse_id != null && warehouse_id != "")
            {
                return _collection.Find(Purchases => Purchases.warehouse_id == warehouse_id).ToList();
            }
            else if (paymentStatus != null && paymentStatus != "")
            {
                return _collection.Find(Purchases => Purchases.payment_status == paymentStatus).ToList();
            }
            else if (statut != null && statut != "")
            {
                return _collection.Find(Purchases => Purchases.statut == statut).ToList();
            }
            else
            {
                return _collection.Find(Purchases => true).ToList();
            }
        }

        public SaleReturns DeleteSaleReturn(string id)
        {
            var saleReturns = _collection.Find(saleReturns => saleReturns.id == id).FirstOrDefault();
            _collection.DeleteOne(SaleReturns => SaleReturns.id == id);
            return saleReturns;
        }

        public bool DeleteMultipleSaleReturn(string[] ids)
        {
            var result = _collection.DeleteMany(SaleReturns => ids.Contains(SaleReturns.id));
            return result.DeletedCount > 0;
        }

    }
}