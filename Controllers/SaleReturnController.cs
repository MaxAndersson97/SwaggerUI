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
    public class SaleReturnController
    {
        private SaleReturnService _service;
        public SaleReturnController(SaleReturnService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<SaleReturns>> GetSaleReturns() => _service.GetSaleReturns();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<SaleReturns> GetSaleReturn(string id) => _service.GetSaleReturn(id);

        //[Authorize]
        [HttpGet("WareHouse/{id:length(24)}")]
        public ActionResult<List<SaleReturns>> GetWareHouseSaleReturns(string id) => _service.GetWareHouseSaleReturns(id);
        //[Authorize]
        [HttpPost]
        public JsonResult PostSaleReturn(SaleReturns user) => _service.PostSaleReturn(user);

        //[Authorize]
        [HttpPost("SaleReturns/Filter")]
        public ActionResult<List<SaleReturns>> GetSalesRetrurnByFilter(string date, string reference, string customer, string returnId, string paymentChoice) => _service.GetSalesRetrurnByFilter(date, reference, customer, returnId, paymentChoice);

        //[Authorize]
        [HttpPost("Filter")]
        public ActionResult<List<SaleReturns>> GetSalesRetrurnByFilter(string date, string reference, string customer, string warehouse_id, string statut, string paymentStatus) => _service.GetSalesRetrurnByFilter(date, reference, customer, warehouse_id, statut, paymentStatus);


        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public JsonResult PutSaleReturn(string id, [FromBody] SaleReturns user) => _service.PutSaleReturn(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<SaleReturns> DeleteSaleReturn(string id) => _service.DeleteSaleReturn(id);
        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleSaleReturn([FromBody] string[] ids) => _service.DeleteMultipleSaleReturn(ids);
    }
}