using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Swagger.Services
{
    public class SaleService
    {
        private readonly IMongoCollection<Sales> _collection;

        public SaleService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Sales>("Sales");
        }

        public List<Sales> GetSales() => _collection.Find(Sales => true).ToList();
        public List<Sales> GetWareHouseSale(string warehouse_id) => _collection.Find(Sales => Sales.warehouse_id == warehouse_id).ToList();

        public Sales GetSale(string id) => _collection.Find(Sales => Sales.id == id).FirstOrDefault();

        public Sales PostSale(Sales sales)
        {
            _collection.InsertOne(sales);
            return sales;
        }
        public List<Sales> GetSalesByFilter(string date, string reference, string customer, string sale, string paymentChoice)
        {
            if (date != null && reference != null && customer != null && sale != null && paymentChoice != null)
            {
                return _collection.Find(Sales => Sales.date == date && Sales.Ref == reference && Sales.client_id == customer && Sales.id == sale && Sales.payment_status == paymentChoice).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Sales => Sales.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Sales => Sales.Ref == reference).ToList();
            }
            else if (customer != null && customer != "")
            {
                return _collection.Find(Sales => Sales.client_id == customer).ToList();
            }
            else if (sale != null && sale != "")
            {
                return _collection.Find(Sales => Sales.id == sale).ToList();
            }
            else if (paymentChoice != null && paymentChoice != "")
            {
                return _collection.Find(Sales => Sales.payment_status == paymentChoice).ToList();
            }
            else
            {
                return _collection.Find(Sales => true).ToList();
            }
        }
        public List<Sales> GetSalesByFilter(string date, string reference, string customer, string warehouse_id, string statut, string paymentStatus)
        {
            if (date != null && reference != null && customer != null && warehouse_id != null && paymentStatus != null && statut != null)
            {
                return _collection.Find(Sales => Sales.date == date && Sales.Ref == reference && Sales.client_id == customer && Sales.warehouse_id == warehouse_id && Sales.payment_status == paymentStatus && Sales.statut == statut).ToList();
            }
            else if (date != null && date != "")
            {
                return _collection.Find(Sales => Sales.date == date).ToList();
            }
            else if (reference != null && reference != "")
            {
                return _collection.Find(Sales => Sales.Ref == reference).ToList();
            }
            else if (customer != null && customer != "")
            {
                return _collection.Find(Sales => Sales.client_id == customer).ToList();
            }
            else if (warehouse_id != null && warehouse_id != "")
            {
                return _collection.Find(Sales => Sales.warehouse_id == warehouse_id).ToList();
            }
            else if (paymentStatus != null && paymentStatus != "")
            {
                return _collection.Find(Sales => Sales.payment_status == paymentStatus).ToList();
            }
            else if (statut != null && statut != "")
            {
                return _collection.Find(Sales => Sales.statut == statut).ToList();
            }
            else
            {
                return _collection.Find(Sales => true).ToList();
            }
        }

        public Sales PutSale(string id, Sales sales)
        {
            _collection.ReplaceOne(sales => sales.id == id, sales);
            return sales;
        }

        public Sales DeleteSale(string id)
        {
            var sales = _collection.Find(sales => sales.id == id).FirstOrDefault();
            _collection.DeleteOne(Sales => Sales.id == id);
            return sales;
        }
        public bool DeleteMultipleSale(string[] ids)
        {
            var result = _collection.DeleteMany(Sales => ids.Contains(Sales.id));
            return result.DeletedCount > 0;
        }

    }
}