using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Swagger.Services
{
    public class PurchaseService
    {
        private readonly IMongoCollection<Purchases> _collection;

        public PurchaseService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Purchases>("Purchases");
        }

        public List<Purchases> GetPurchases() => _collection.Find(Purchases => true).ToList();

        public Purchases GetPurchase(string id) => _collection.Find(Purchases => Purchases.id == id).FirstOrDefault();

        public List<Purchases> GetPurchaseOfWareHouse(string warehouse_id) => _collection.Find(Purchases => Purchases.warehouse_id == warehouse_id).ToList();


        public List<Purchases> GetPurchasesByFilter(string date, string reference, string supplier, string purchaseId, string paymentChoice)
        {
            if (date != null && reference != null && supplier != null && purchaseId != null && paymentChoice != null)
            {
                return _collection.Find(Purchases => Purchases.date == date && Purchases.Ref == reference && Purchases.provider_id == supplier && Purchases.id == purchaseId && Purchases.payment_status == paymentChoice).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Purchases => Purchases.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Purchases => Purchases.Ref == reference).ToList();
            }
            else if (supplier != null && supplier != "")
            {
                return _collection.Find(Purchases => Purchases.provider_id == supplier).ToList();
            }
            else if (purchaseId != null && purchaseId != "")
            {
                return _collection.Find(Purchases => Purchases.id == purchaseId).ToList();
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

        public List<Purchases> GetPurchasesByFilter(string date, string reference, string supplier, string warehouse_id, string statut, string paymentStatus)
        {
            if (date != null && reference != null && supplier != null && warehouse_id != null && paymentStatus != null && statut != null)
            {
                return _collection.Find(Purchases => Purchases.date == date && Purchases.Ref == reference && Purchases.provider_id == supplier && Purchases.id == warehouse_id && Purchases.payment_status == paymentStatus && Purchases.statut == statut).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Purchases => Purchases.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Purchases => Purchases.Ref == reference).ToList();
            }
            else if (supplier != null && supplier != "")
            {
                return _collection.Find(Purchases => Purchases.provider_id == supplier).ToList();
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

        public Purchases PostPurchase(Purchases Purchases)
        {
            _collection.InsertOne(Purchases);
            return Purchases;
        }

        public Purchases PutPurchase(string id, Purchases Purchases)
        {
            _collection.ReplaceOne(Purchases => Purchases.id == id, Purchases);
            return Purchases;
        }

        public Purchases DeletePurchase(string id)
        {
            var Purchases = _collection.Find(Purchases => Purchases.id == id).FirstOrDefault();
            _collection.DeleteOne(Purchases => Purchases.id == id);
            return Purchases;
        }
        public bool DeleteMultiplePurchase(string[] ids)
        {
            var result = _collection.DeleteMany(Purchases => ids.Contains(Purchases.id));
            return result.DeletedCount > 0;
        }

    }
}