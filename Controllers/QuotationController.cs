using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class QuotationController
    {
        private QuotationService _service;
        public QuotationController(QuotationService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Quotations>> GetQuotations() => _service.GetQuotations();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Quotations> GetQuotation(string id) => _service.GetQuotation(id);

        //[Authorize]
        [HttpGet("Filter")]
        public ActionResult<List<Quotations>> GetFilterQoutations(string date, string refrence, string customer, string warehouse_id, string statut) => _service.GetFilterQoutations(date, refrence, customer, warehouse_id, statut);
        //[Authorize]
        [HttpPost]
        public JsonResult PostQuotation(Quotations user) => _service.PostQuotation(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public JsonResult PutQuotation(string id, Quotations user) => _service.PutQuotation(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Quotations> DeleteQuotation(string id) => _service.DeleteQuotation(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleQuotation([FromBody] string[] ids) => _service.DeleteMultipleQuotation(ids);
    }
}