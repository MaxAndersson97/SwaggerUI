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
    public class PurchaseReturnController
    {
        private PurchaseReturnService _service;
        public PurchaseReturnController(PurchaseReturnService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PurchaseReturn>> GetPurchaseReturns() => _service.GetPurchaseReturns();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PurchaseReturn> GetPurchaseReturn(string id) => _service.GetPurchaseReturn(id);

        //[Authorize]
        [HttpGet("Warehouse/{id:length(24)}")]
        public ActionResult<List<PurchaseReturn>> GetWareHousePurchaseReturn(string id) => _service.GetWareHousePurchaseReturn(id);
        //[Authorize]
        [HttpPost]
        public JsonResult PostPurchaseReturn([FromBody] PurchaseReturn user) => _service.PostPurchaseReturn(user);

        //[Authorize]
        [HttpPost("PurchaseReturn/Filter")]
        public ActionResult<List<PurchaseReturn>> GetPurchaseReturnsByFilter(string date, string reference, string supplier, string returnId, string paymentChoice) => _service.GetPurchaseReturnsByFilter(date, reference, supplier, returnId, paymentChoice);

        //[Authorize]
        [HttpPost("Filter")]
        public ActionResult<List<PurchaseReturn>> GetPurchaseReturnsByFilter(string date, string reference, string supplier, string warehouse_id, string statut, string paymentStatus) => _service.GetPurchaseReturnsByFilter(date, reference, supplier, warehouse_id, statut, paymentStatus);


        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public JsonResult PutPurchaseReturn(string id, [FromBody] PurchaseReturn user) => _service.PutPurchaseReturn(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PurchaseReturn> DeletePurchaseReturn(string id) => _service.DeletePurchaseReturn(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultiplePurchaseReturn([FromBody] string[] ids) => _service.DeleteMultiplePurchaseReturn(ids);
    }
}