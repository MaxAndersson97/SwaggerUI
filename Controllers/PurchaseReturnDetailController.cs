using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseReturnDetailController
    {
        private PurchaseReturnDetailService _service;
        public PurchaseReturnDetailController(PurchaseReturnDetailService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PurchaseReturnDetails>> GetPurchaseReturnDetails() => _service.GetPurchaseReturnDetails();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PurchaseReturnDetails> GetPurchaseReturnDetail(string id) => _service.GetPurchaseReturnDetail(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PurchaseReturnDetails> PostPurchaseReturnDetail(PurchaseReturnDetails user) => _service.PostPurchaseReturnDetail(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PurchaseReturnDetails> PutPurchaseReturnDetail(string id, PurchaseReturnDetails user) => _service.PutPurchaseReturnDetail(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PurchaseReturnDetails> DeletePurchaseReturnDetail(string id) => _service.DeletePurchaseReturnDetail(id);
    }
}