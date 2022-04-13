using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseDetailController
    {
        private PurchaseDetailService _service;
        public PurchaseDetailController(PurchaseDetailService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PurchaseDetails>> GetPurchaseDetails() => _service.GetPurchaseDetails();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PurchaseDetails> GetPurchaseDetail(string id) => _service.GetPurchaseDetail(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PurchaseDetails> PostPurchaseDetail(PurchaseDetails user) => _service.PostPurchaseDetail(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PurchaseDetails> PutPurchaseDetail(string id, PurchaseDetails user) => _service.PutPurchaseDetail(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PurchaseDetails> DeletePurchaseDetail(string id) => _service.DeletePurchaseDetail(id);
    }
}