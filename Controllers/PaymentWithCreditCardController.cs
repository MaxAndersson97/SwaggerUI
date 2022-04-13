using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentWithCreditCardController
    {
        private PaymentWithCreditCardService _service;
        public PaymentWithCreditCardController(PaymentWithCreditCardService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PaymentWithCreditCard>> GetPaymentWithCreditCards() => _service.GetPaymentWithCreditCards();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PaymentWithCreditCard> GetPaymentWithCreditCard(string id) => _service.GetPaymentWithCreditCard(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PaymentWithCreditCard> PostPaymentWithCreditCard(PaymentWithCreditCard user) => _service.PostPaymentWithCreditCard(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PaymentWithCreditCard> PutPaymentWithCreditCard(string id, PaymentWithCreditCard user) => _service.PutPaymentWithCreditCard(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PaymentWithCreditCard> DeletePaymentWithCreditCard(string id) => _service.DeletePaymentWithCreditCard(id);
    }
}