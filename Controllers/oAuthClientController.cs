using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class oAuthClientController
    {
        private oAuthClientService _service;
        public oAuthClientController(oAuthClientService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<oAuthClients>> GetoAuthClients() => _service.GetoAuthClients();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<oAuthClients> GetoAuthClient(string id) => _service.GetoAuthClient(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<oAuthClients> PostoAuthClient(oAuthClients oAuthClient) => _service.PostoAuthClient(oAuthClient);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<oAuthClients> PutoAuthClient(string id, oAuthClients oAuthClient) => _service.PutoAuthClient(id, oAuthClient);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<oAuthClients> DeleteoAuthClient(string id) => _service.DeleteoAuthClient(id);
    }
}