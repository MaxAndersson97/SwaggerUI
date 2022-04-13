using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Swagger.Services
{
    public class TransferService
    {
        private readonly IMongoCollection<Transfers> _collection;
        private readonly IMongoCollection<TransferDetails> _collectionTransferDetails;

        public TransferService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Transfers>("Transfers");
            _collectionTransferDetails = database.GetCollection<TransferDetails>("TransferDetails");
        }

        public List<Transfers> GetTransfers() => _collection.Find(Transfers => true).ToList();
       
        public List<Transfers> GetFilterTransfers(string referance, string fromWareHouse, string toWareHouse, string statut)
        {
            if (referance != null && fromWareHouse != null && toWareHouse != null && statut != null)
            {

                return _collection.Find(Transfers => Transfers.Ref == referance && Transfers.from_warehouse_id == fromWareHouse && Transfers.to_warehouse_id == toWareHouse && Transfers.statut == statut).ToList();
            }
            else if (referance != null)
            {
                return _collection.Find(Transfers => Transfers.Ref == referance).ToList();
            }
            else if (fromWareHouse != null)
            {
                return _collection.Find(Transfers => Transfers.from_warehouse_id == fromWareHouse).ToList();
            }
            else if (toWareHouse != null)
            {
                return _collection.Find(Transfers => Transfers.to_warehouse_id == toWareHouse).ToList();
            }
            else if (statut != null)
            {
                return _collection.Find(Transfers => Transfers.statut == statut).ToList();
            }
            else
            {
                return _collection.Find(Transfers => true).ToList();
            }
        }

        public JsonResult GetTransfer(string id)
        {
            Transfers transfer = _collection.Find(Transfers => Transfers.id == id).FirstOrDefault();
            if (transfer == null)
            {
                return new JsonResult("No transfer found");
            }
            else
            {
              /*  JArray json = new();
                foreach (var item in transfer.transfer_details_id)
                {
                    TransferDetails transferDetails = _collectionTransferDetails.Find(TransferDetails => TransferDetails.id == item).FirstOrDefault();
                    json.Add(JObject.FromObject(transferDetails));
                }
                JObject jsonObject = new();
                jsonObject.Add("id", transfer.id);
                jsonObject.Add("user_id", transfer.user_id);
                jsonObject.Add("date", transfer.date);
                jsonObject.Add("ref", transfer.Ref);
                jsonObject.Add("from_warehouse_id", transfer.from_warehouse_id);
                jsonObject.Add("to_warehouse_id", transfer.to_warehouse_id);
                jsonObject.Add("statut", transfer.statut);
                jsonObject.Add("items", transfer.items);
                jsonObject.Add("tax_rate", transfer.tax_rate);
                jsonObject.Add("TaxNet", transfer.TaxNet);
                jsonObject.Add("discount", transfer.discount);
                jsonObject.Add("shipping", transfer.shipping);
                jsonObject.Add("GrandTotal", transfer.GrandTotal);
                jsonObject.Add("notes", transfer.notes);
                jsonObject.Add("transfer_details", json);*/
                return new JsonResult(new { statut = 200, data = transfer });
            }
        }

        public JsonResult PostTransfer(Transfers json)
        {
          /*  Transfers transfers = new();
            transfers.user_id = json["user_id"].ToString();
            transfers.date = json["date"].ToString();
            transfers.Ref = json["ref"].ToString();
            transfers.from_warehouse_id = json["from_warehouse_id"].ToString();
            transfers.to_warehouse_id = json["to_warehouse_id"].ToString();
            transfers.statut = json["statut"].ToString();
            transfers.items = double.Parse(json["items"].ToString());
            transfers.tax_rate = double.Parse(json["tax_rate"].ToString());
            transfers.TaxNet = double.Parse(json["TaxNet"].ToString());
            transfers.discount = double.Parse(json["discount"].ToString());
            transfers.shipping = double.Parse(json["shipping"].ToString());
            transfers.GrandTotal = double.Parse(json["GrandTotal"].ToString());
            transfers.notes = json["notes"].ToString();
            List<TransferDetails> transferDetails = json["transfers_detail"].ToObject<List<TransferDetails>>();
            _collectionTransferDetails.InsertMany(transferDetails);
            transfers.transfer_details_id = transferDetails.Select(x => x.id).ToList(); */
            _collection.InsertOne(json);

            return new JsonResult(new { message = "Create Successfully", statut = 201 });
        }

        public JsonResult PutTransfer(string id, Transfers transfers)
        {
            Transfers transfer = _collection.Find(Transfers => Transfers.id == id).FirstOrDefault();
            if (transfer == null)
            {
                return new JsonResult("No transfer found");
            }
            else
            {
               /* Transfers newTransfer = new();
                newTransfer.id = transfers["id"].ToString();
                newTransfer.user_id = transfers["user_id"].ToString();
                newTransfer.date = transfers["date"].ToString();
                newTransfer.Ref = transfers["Ref"].ToString();
                newTransfer.from_warehouse_id = transfers["from_warehouse_id"].ToString();
                newTransfer.to_warehouse_id = transfers["to_warehouse_id"].ToString();
                newTransfer.statut = transfers["statut"].ToString();
                newTransfer.items = double.Parse(transfers["items"].ToString());
                newTransfer.tax_rate = double.Parse(transfers["tax_rate"].ToString());
                newTransfer.TaxNet = double.Parse(transfers["TaxNet"].ToString());
                newTransfer.discount = double.Parse(transfers["discount"].ToString());
                newTransfer.shipping = double.Parse(transfers["shipping"].ToString());
                newTransfer.GrandTotal = double.Parse(transfers["GrandTotal"].ToString());
                newTransfer.notes = transfers["notes"].ToString();
                List<TransferDetails> transferDetails = transfers["transfer_details"].ToObject<List<TransferDetails>>();
                foreach (var item in transferDetails)
                {
                    if (_collectionTransferDetails.Find(TransferDetails => TransferDetails.id == item.id).FirstOrDefault() == null)
                    {
                        _collectionTransferDetails.InsertOne(item);
                    }
                    else
                    {
                        _collectionTransferDetails.ReplaceOne(TransferDetails => TransferDetails.id == item.id, item);
                    }
                }
                newTransfer.transfer_details_id = transferDetails.Select(x => x.id).ToList();*/
                _collection.ReplaceOne(Transfers => Transfers.id == id, transfers);
                return new JsonResult(new { message = "Update Successfully", statut = 200 });
            }
        }

        public Transfers DeleteTransfer(string id)
        {
            var transfers = _collection.Find(transfers => transfers.id == id).FirstOrDefault();
            _collection.DeleteOne(Transfers => Transfers.id == id);
            return transfers;
        }

        public bool DeleteMultipleTransfer(string[] ids)
        {
            var result = _collection.DeleteMany(Transfers => ids.Contains(Transfers.id));
            return result.DeletedCount > 0;
        }

    }
}