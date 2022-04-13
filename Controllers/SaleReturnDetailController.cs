using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SaleReturnDetailController
    {
        private SaleReturnDetailService _service;
        public SaleReturnDetailController(SaleReturnDetailService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<SaleReturnDetails>> GetSaleReturnDetails() => _service.GetSaleReturnDetails();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<SaleReturnDetails> GetSaleReturnDetail(string id) => _service.GetSaleReturnDetail(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<SaleReturnDetails> PostSaleReturnDetail(SaleReturnDetails user) => _service.PostSaleReturnDetail(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<SaleReturnDetails> PutSaleReturnDetail(string id, SaleReturnDetails user) => _service.PutSaleReturnDetail(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<SaleReturnDetails> DeleteSaleReturnDetail(string id) => _service.DeleteSaleReturnDetail(id);
    }
}