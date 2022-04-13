using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentSaleReturnController
    {
        private PaymentSaleReturnService _service;
        public PaymentSaleReturnController(PaymentSaleReturnService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PaymentSaleReturns>> GetPaymentSaleReturns() => _service.GetPaymentSaleReturns();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PaymentSaleReturns> GetPaymentSaleReturn(string id) => _service.GetPaymentSaleReturn(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PaymentSaleReturns> PostPaymentSaleReturn(PaymentSaleReturns user) => _service.PostPaymentSaleReturn(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PaymentSaleReturns> PutPaymentSaleReturn(string id, PaymentSaleReturns user) => _service.PutPaymentSaleReturn(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PaymentSaleReturns> DeletePaymentSaleReturn(string id) => _service.DeletePaymentSaleReturn(id);
    }
}