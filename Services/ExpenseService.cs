using Swagger.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace Swagger.Services
{
    public class ExpenseService
    {
        private readonly IMongoCollection<Expense> _collection;

        public ExpenseService(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetConnectionString("DB"));
            var database = Client.GetDatabase("stocko_db");

            _collection = database.GetCollection<Expense>("Expense");
        }

        public List<Expense> GetExpenses() => _collection.Find(Expense => true).ToList();
        //public List<Expense> GetFilterExpense(string date, string reference, string warehouse, string expenseCategory)
        //{
        //   try
        //    {
        //        if (expenseCategory == "undefined")
        //        {
        //            expenseCategory = null;
        //        }
        //        if (date != null && reference != null && warehouse != null && expenseCategory != null)
        //        {
        //            return _collection.Find(Expense => Expense.date == date && Expense.Ref == reference && Expense.warehouse_id == warehouse &&
        //             Expense.expense_category_id == expenseCategory).ToList();
        //        }
        //        else if (date != null)
        //        {
        //            return _collection.Find(Expense => Expense.date == date).ToList();
        //        }
        //        else if (reference != null)
        //        {
        //            return _collection.Find(Expense => Expense.Ref == reference).ToList();
        //        }
        //        else if (warehouse != null)
        //        {
        //            return _collection.Find(Expense => Expense.warehouse_id == warehouse).ToList();
        //        }
        //        else if (expenseCategory != null)
        //        {
        //            return _collection.Find(Expense => Expense.expense_category_id == expenseCategory).ToList();
        //        }
        //        else
        //        {
        //            return _collection.Find(Expense => true).ToList();
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }

            

            
        //}

        public List<Expense> GetFilterExpense(string date, string reference, string warehouse, string expenseCategory)
        {
            if (date != null  && reference != null && warehouse != null && expenseCategory != null)
            {

                return _collection.Find(Expense => Expense.date == date && Expense.Ref == reference && Expense.warehouse_id== warehouse && Expense.expense_category_id == expenseCategory).ToList();
            }
            else if (date != null)
            {
                return _collection.Find(Expense => Expense.date == date).ToList();
            }
            else if (reference != null)
            {
                return _collection.Find(Expense => Expense.Ref == reference).ToList();
            }
            else if (warehouse != null)
            {
                return _collection.Find(Expense => Expense.warehouse_id == warehouse).ToList();
            }
            else if (expenseCategory != null)
            {
                return _collection.Find(Expense => Expense.expense_category_id == expenseCategory).ToList();
            }
            else
            {
                return _collection.Find(Expense => true).ToList();
            }
        }

        public List<Expense> GetWareHouseExpense(string id) => _collection.Find(Expense => Expense.warehouse_id == id).ToList();

        public Expense GetExpense(string id) => _collection.Find(Expense => Expense.id == id).FirstOrDefault();

        public JsonResult PostExpense(Expense expense)
        {
            _collection.InsertOne(expense);
            return new JsonResult(new { message = "Create Successfully", statut = 201 });
        }

        public Expense PutExpense(string id, Expense expense)
        {
            _collection.ReplaceOne(expense => expense.id == id, expense);
            return expense;
        }

        public Expense DeleteExpense(string id)
        {
            var expense = _collection.Find(expense => expense.id == id).FirstOrDefault();
            _collection.DeleteOne(Expense => Expense.id == id);
            return expense;
        }

        public bool DeleteMultipleExpense(string[] ids)
        {
            var result = _collection.DeleteMany(Expense => ids.Contains(Expense.id));
            return result.DeletedCount > 0;
        }

    }
}