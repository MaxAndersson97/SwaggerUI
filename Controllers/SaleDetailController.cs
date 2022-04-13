using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SaleDetailController
    {
        private SaleDetailService _service;
        public SaleDetailController(SaleDetailService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<SaleDetails>> GetSaleDetails() => _service.GetSaleDetails();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<SaleDetails> GetSaleDetail(string id) => _service.GetSaleDetail(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<SaleDetails> PostSaleDetail(SaleDetails user) => _service.PostSaleDetail(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<SaleDetails> PutSaleDetail(string id, SaleDetails user) => _service.PutSaleDetail(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<SaleDetails> DeleteSaleDetail(string id) => _service.DeleteSaleDetail(id);
    }
}