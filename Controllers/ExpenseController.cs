using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController
    {
        private ExpenseService _service;
        public ExpenseController(ExpenseService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Expense>> GetExpenses() => _service.GetExpenses();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Expense> GetExpense(string id) => _service.GetExpense(id);

        //[Authorize]
        [HttpGet("WareHouse/{id:length(24)}")]
        public ActionResult<List<Expense>> GetWareHouseExpense(string id) => _service.GetWareHouseExpense(id);

        //[Authorize]
        [HttpGet("Filter")]
        //public ActionResult<List<Expense>> GetFilterExpense(string date, string reference, string warehouse, string expenseCetory) => _service.GetFilterExpense(date, reference, warehouse, expenseCetory);

        public ActionResult<List<Expense>> GetFilterExpense(string? date, string? reference, string? warehouse, string? expenseCetory) => _service.GetFilterExpense(date, reference, warehouse, expenseCetory);
        //[Authorize]
        [HttpPost]
        public JsonResult PostExpense(Expense expense) => _service.PostExpense(expense);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Expense> PutExpense(string id, Expense expense) => _service.PutExpense(id, expense);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Expense> DeleteExpense(string id) => _service.DeleteExpense(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleExpense([FromBody] string[] ids) => _service.DeleteMultipleExpense(ids);
    }
}