using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;
using Newtonsoft.Json.Linq;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class AdjustmentController
    {
        private AdjustmentService _service;
        public AdjustmentController(AdjustmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Adjustments>> GetAdjustmentss()
        {
            return _service.GetAdjustments();
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Adjustments> GetAdjustments(string id) => _service.GetAdjustment(id);

        [HttpGet("Filter")]
        public ActionResult<List<Adjustments>> GetFilterAdjustments(DateTime? date, string reference, string warehouse) => _service.GetFilterAdjustments(date, reference, warehouse);

        [HttpPost]
        public JsonResult PostAdjustments([FromBody] Adjustments json)
        {
            return _service.PostAdjustments(json);
        }

        [HttpPut("{id:length(24)}")]
        public JsonResult PutAdjustments(string id, [FromBody] Adjustments json) => _service.PutAdjustments(id, json);

        [HttpDelete("{id:length(24)}")]
        public ActionResult<Adjustments> DeleteAdjustments(string id) => _service.DeleteAdjustments(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleAdjustments([FromBody] string[] ids) => _service.DeleteMultipleAdjustments(ids);
    }
}