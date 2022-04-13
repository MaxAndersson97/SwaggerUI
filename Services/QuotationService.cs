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
    public class QuotationService
    {
        private readonly IMongoCollection<Quotations> _collection;
        private readonly IMongoCollection<QuotationDetails> _collectionQuotationDetails;

        public QuotationService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collectionQuotationDetails = database.GetCollection<QuotationDetails>("QuotationDetails");
            _collection = database.GetCollection<Quotations>("Quotations");
        }

        public List<Quotations> GetQuotations() => _collection.Find(Quotations => true).ToList();
        public List<Quotations> GetFilterQoutations(string date, string reference, string customer, string warehouse_id, string statut)
        {
            if (date != null && reference != null && warehouse_id != null && statut != null && customer != null)
            {
                return _collection.Find(Expense => Expense.date == date && Expense.Ref == reference && Expense.warehouse_id == warehouse_id && Expense.statut == statut && Expense.client_id == customer).ToList();
            }
            else if (date != null)
            {
                return _collection.Find(Expense => Expense.date == date).ToList();
            }
            else if (reference != null)
            {
                return _collection.Find(Expense => Expense.Ref == reference).ToList();
            }
            else if (warehouse_id != null)
            {
                return _collection.Find(Expense => Expense.warehouse_id == warehouse_id).ToList();
            }
            else if (statut != null)
            {
                return _collection.Find(Expense => Expense.statut == statut).ToList();
            }
            else if (customer != null)
            {
                return _collection.Find(Expense => Expense.client_id == customer).ToList();
            }
            else
            {
                return _collection.Find(Expense => true).ToList();
            }
        }

        public JsonResult GetQuotation(string id)
        {

            Quotations quotation = _collection.Find(Expense => Expense.id == id).FirstOrDefault();
            if (quotation == null)
            {
                return new JsonResult(new { message = "No se encontro la cotizacion" });
            }
            else
            {
                /*JArray jArray = new();
                foreach (var item in quotation.quotation_detail_id)
                {
                    QuotationDetails quotationDetails = _collectionQuotationDetails.Find(QuotationDetails => QuotationDetails.id == item).FirstOrDefault();
                    jArray.Add(JObject.FromObject(quotationDetails));
                }
                JObject jObject = new();
                jObject.Add("user_id", quotation.user_id);
                jObject.Add("date", quotation.date);
                jObject.Add("Ref", quotation.Ref);
                jObject.Add("client_id", quotation.client_id);
                jObject.Add("warehouse_id", quotation.warehouse_id);
                jObject.Add("tax_rate", quotation.tax_rate);
                jObject.Add("TaxNet", quotation.TaxNet);
                jObject.Add("discount", quotation.discount);
                jObject.Add("shipping", quotation.shipping);
                jObject.Add("GrandTotal", quotation.GrandTotal);
                jObject.Add("notes", quotation.notes);
                jObject.Add("statut", quotation.statut);
                jObject.Add("quotation_detail", jArray);*/

                return new JsonResult(new { statut = 200, data = quotation });
            }

        }
        public JsonResult PostQuotation(Quotations json)
        {
            /*Quotations quotations = new();
            quotations.user_id = json["user_id"].ToString();
            quotations.date = json["date"].ToString();
            quotations.Ref = json["ref"].ToString();
            quotations.client_id = json["client_id"].ToString();
            quotations.warehouse_id = json["warehouse_id"].ToString();
            quotations.tax_rate = double.Parse(json["tax_rate"].ToString());
            quotations.TaxNet = double.Parse(json["TaxNet"].ToString());
            quotations.discount = double.Parse(json["discount"].ToString());
            quotations.shipping = double.Parse(json["shipping"].ToString());
            quotations.GrandTotal = double.Parse(json["GrandTotal"].ToString());
            quotations.notes = json["notes"].ToString();
            quotations.statut = json["statut"].ToString();
            quotations.created_at = DateTime.Now.ToString();
            quotations.updated_at = DateTime.Now.ToString();

            List<QuotationDetails> quotationsDetails = json["quotation_detail"].ToObject<List<QuotationDetails>>();
            _collectionQuotationDetails.InsertMany(quotationsDetails);
            quotations.quotation_detail_id = quotationsDetails.Select(x => x.id).ToList();*/
            _collection.InsertOne(json);

            return new JsonResult(new { message = "Create Successfully", statut = 201 });
        }

        public JsonResult PutQuotation(string id, Quotations json)
        {
            Quotations quotations = _collection.Find(Quotation => Quotation.id == id).FirstOrDefault();
            if (quotations == null)
            {
                return new JsonResult(new { message = "No se encontro la cotizacion", statut = 404 });
            }
            else
            {
                /*quotations.user_id = json["user_id"].ToString();
                quotations.date = json["date"].ToString();
                quotations.Ref = json["ref"].ToString();
                quotations.client_id = json["client_id"].ToString();
                quotations.warehouse_id = json["warehouse_id"].ToString();
                quotations.tax_rate = double.Parse(json["tax_rate"].ToString());
                quotations.TaxNet = double.Parse(json["TaxNet"].ToString());
                quotations.discount = double.Parse(json["discount"].ToString());
                quotations.shipping = double.Parse(json["shipping"].ToString());
                quotations.GrandTotal = double.Parse(json["GrandTotal"].ToString());
                quotations.notes = json["notes"].ToString();
                quotations.statut = json["statut"].ToString();
                quotations.updated_at = DateTime.Now.ToString();

                List<QuotationDetails> quotationsDetails = json["quotation_detail"].ToObject<List<QuotationDetails>>();
                foreach (var item in quotationsDetails)
                {
                    if (_collectionQuotationDetails.Find(QuotationDetails => QuotationDetails.id == item.id).FirstOrDefault() == null)
                    {
                        _collectionQuotationDetails.InsertOne(item);
                    }
                    else
                    {
                        _collectionQuotationDetails.ReplaceOne(QuotationDetails => QuotationDetails.id == item.id, item);
                    }
                }
                quotations.quotation_detail_id = quotationsDetails.Select(x => x.id).ToList();*/


                _collection.ReplaceOne(Quotation => Quotation.id == id, json);

                return new JsonResult(new { message = "Update Successfully", statut = 200 });
            }
        }

        public Quotations DeleteQuotation(string id)
        {
            var Quotations = _collection.Find(Quotations => Quotations.id == id).FirstOrDefault();
            _collection.DeleteOne(Quotations => Quotations.id == id);
            return Quotations;
        }

        public bool DeleteMultipleQuotation(string[] ids)
        {
            var result = _collection.DeleteMany(ExpenseCategories => ids.Contains(ExpenseCategories.id));
            return result.DeletedCount > 0;
        }

    }
}