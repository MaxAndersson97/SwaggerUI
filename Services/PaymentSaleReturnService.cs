using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Swagger.Services
{
    public class PaymentSaleReturnService
    {
        private readonly IMongoCollection<PaymentSaleReturns> _collection;

        public PaymentSaleReturnService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<PaymentSaleReturns>("PaymentSaleReturns");
        }

        public List<PaymentSaleReturns> GetPaymentSaleReturns() => _collection.Find(PaymentSaleReturns => true).ToList();

        public PaymentSaleReturns GetPaymentSaleReturn(string id) => _collection.Find(PaymentSaleReturns => PaymentSaleReturns.id == id).FirstOrDefault();

        public PaymentSaleReturns PostPaymentSaleReturn(PaymentSaleReturns PaymentSaleReturns)
        {
            _collection.InsertOne(PaymentSaleReturns);
            return PaymentSaleReturns;
        }

        public PaymentSaleReturns PutPaymentSaleReturn(string id, PaymentSaleReturns PaymentSaleReturns)
        {
            _collection.ReplaceOne(PaymentSaleReturns => PaymentSaleReturns.id == id, PaymentSaleReturns);
            return PaymentSaleReturns;
        }

        public PaymentSaleReturns DeletePaymentSaleReturn(string id)
        {
            var PaymentSaleReturns = _collection.Find(PaymentSaleReturns => PaymentSaleReturns.id == id).FirstOrDefault();
            _collection.DeleteOne(PaymentSaleReturns => PaymentSaleReturns.id == id);
            return PaymentSaleReturns;
        }

    }
}