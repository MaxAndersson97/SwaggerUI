using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController
    {
        private TransferService _service;
        public TransferController(TransferService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Transfers>> GetTransfers() => _service.GetTransfers();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Transfers> GetTransfer(string id) => _service.GetTransfer(id);
        //[Authorize]
        [HttpPost]
        public JsonResult PostTransfer(Transfers json) => _service.PostTransfer(json);
        [HttpPost("Filter")]
        public ActionResult<List<Transfers>> GetFilterTransfers(string referance, string fromWareHouse, string toWareHouse, string statut) => _service.GetFilterTransfers(referance, fromWareHouse, toWareHouse, statut);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public JsonResult PutTransfer(string id, Transfers user) => _service.PutTransfer(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Transfers> DeleteTransfer(string id) => _service.DeleteTransfer(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleTransfer([FromBody] string[] ids) => _service.DeleteMultipleTransfer(ids);
    }
}