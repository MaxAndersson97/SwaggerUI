using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System;

namespace Swagger.Services
{
    public class PaymentPurchaseReturnService
    {
        private readonly IMongoCollection<PaymentPurchaseReturns> _collection;

        public PaymentPurchaseReturnService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<PaymentPurchaseReturns>("PaymentPurchaseReturns");
        }

        public List<PaymentPurchaseReturns> GetPaymentPurchaseReturns() => _collection.Find(PaymentPurchaseReturns => true).ToList();

        public PaymentPurchaseReturns GetPaymentPurchaseReturn(string id) => _collection.Find(PaymentPurchaseReturns => PaymentPurchaseReturns.id == id).FirstOrDefault();

        public PaymentPurchaseReturns PostPaymentPurchaseReturn(PaymentPurchaseReturns PaymentPurchaseReturns)
        {

            PaymentPurchaseReturns.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.InsertOne(PaymentPurchaseReturns);
            return PaymentPurchaseReturns;
        }

        public PaymentPurchaseReturns PutPaymentPurchaseReturn(string id, PaymentPurchaseReturns PaymentPurchaseReturns)
        {
            PaymentPurchaseReturns.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _collection.ReplaceOne(PaymentPurchaseReturns => PaymentPurchaseReturns.id == id, PaymentPurchaseReturns);
            return PaymentPurchaseReturns;
        }

        public PaymentPurchaseReturns DeletePaymentPurchaseReturn(string id)
        {
            var PaymentPurchaseReturns = _collection.Find(PaymentPurchaseReturns => PaymentPurchaseReturns.id == id).FirstOrDefault();
            _collection.DeleteOne(PaymentPurchaseReturns => PaymentPurchaseReturns.id == id);
            return PaymentPurchaseReturns;
        }

    }
}