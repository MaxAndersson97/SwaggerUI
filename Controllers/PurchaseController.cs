using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController
    {
        private PurchaseService _service;
        public PurchaseController(PurchaseService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Purchases>> GetPurchases() => _service.GetPurchases();
        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Purchases> GetPurchase(string id) => _service.GetPurchase(id);
        //[Authorize]
        [HttpGet("WareHouse/{id:length(24)}")]
        public ActionResult<List<Purchases>> GetPurchaseOfWareHouse(string warehouse_id) => _service.GetPurchaseOfWareHouse(warehouse_id);
        //[Authorize]
        [HttpPost("Purchase/Filter")]
        public ActionResult<List<Purchases>> GetPurchasesByFilter(string date, string reference, string supplier, string purchase, string paymentChoice) => _service.GetPurchasesByFilter(date, reference, supplier, purchase, paymentChoice);
        //[Authorize]
        [HttpPost("Filter")]
        public ActionResult<List<Purchases>> GetPurchasesByFilter(string date, string reference, string supplier, string warehouse_id, string statut, string paymentStatus) => _service.GetPurchasesByFilter(date, reference, supplier, warehouse_id, statut, paymentStatus);

        //[Authorize]
        [HttpPost]
        public ActionResult<Purchases> PostPurchase(Purchases user) => _service.PostPurchase(user);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Purchases> PutPurchase(string id, Purchases user) => _service.PutPurchase(id, user);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Purchases> DeletePurchase(string id) => _service.DeletePurchase(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultiplePurchase([FromBody] string[] ids) => _service.DeleteMultiplePurchase(ids);
    }
}