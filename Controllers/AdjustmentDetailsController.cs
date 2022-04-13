using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AdjustmentDetailsController
    {
        private AdjustmentDetailsService _service;
        public AdjustmentDetailsController(AdjustmentDetailsService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<AdjustmentDetails>> GetAdjustmentDetailss() => _service.GetAdjustmentDetails();
        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<AdjustmentDetails> GetAdjustmentDetails(string id) => _service.GetAdjustmentDetail(id);

        //[Authorize]
        [HttpPost("/AdjustmentDetails/Filter")]
        // public ActionResult<List<AdjustmentDetails>> GetAdjustmentDetailsByFilter(string date, string reference, string supplier, string purchase, string paymentChoice) => _service.GetAdjustmentDetailsByFilter(date, reference, supplier, purchase, paymentChoice);
        //[Authorize]

        [HttpPut("{id:length(24)}")]
        public ActionResult<AdjustmentDetails> PutAdjustmentDetails(string id, AdjustmentDetails adjustmentDetails) => _service.PutAdjustmentDetails(id, adjustmentDetails);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<AdjustmentDetails> DeleteAdjustmentDetails(string id) => _service.DeleteAdjustmentDetails(id);
    }
}