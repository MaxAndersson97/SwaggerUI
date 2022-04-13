using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentSaleController
    {
        private PaymentSaleService _service;
        public PaymentSaleController(PaymentSaleService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PaymentSales>> GetPaymentSales() => _service.GetPaymentSales();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PaymentSales> GetPaymentSale(string id) => _service.GetPaymentSale(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PaymentSales> PostPaymentSale(PaymentSales user) => _service.PostPaymentSale(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PaymentSales> PutPaymentSale(string id, PaymentSales user) => _service.PutPaymentSale(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PaymentSales> DeletePaymentSale(string id) => _service.DeletePaymentSale(id);
    }
}