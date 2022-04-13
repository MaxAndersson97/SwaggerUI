using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseCategorieController
    {
        private ExpenseCategorieService _service;
        public ExpenseCategorieController(ExpenseCategorieService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<ExpenseCategories>> GetExpenseCategories() => _service.GetExpenseCategories();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<ExpenseCategories> GetExpenseCategory(string id) => _service.GetExpenseCategory(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<ExpenseCategories> PostExpenseCategory(ExpenseCategories user) => _service.PostExpenseCategory(user);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<ExpenseCategories> PutExpenseCategory(string id, ExpenseCategories user) => _service.PutExpenseCategory(id, user);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<ExpenseCategories> DeleteExpenseCategory(string id) => _service.DeleteExpenseCategory(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleExpenseCategory([FromBody] string[] ids) => _service.DeleteMultipleExpenseCategory(ids);
    }
}