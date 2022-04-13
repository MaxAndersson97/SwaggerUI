using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class oAuthPersonalAccessClientController
    {
        private oAuthPersonalAccessClientService _service;
        public oAuthPersonalAccessClientController(oAuthPersonalAccessClientService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<oAuthPersonalAccessClients>> GetoAuthPersonalAccessClients() => _service.GetoAuthPersonalAccessClients();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<oAuthPersonalAccessClients> GetoAuthPersonalAccessClient(string id) => _service.GetoAuthPersonalAccessClient(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<oAuthPersonalAccessClients> PostoAuthPersonalAccessClient(oAuthPersonalAccessClients user) => _service.PostoAuthPersonalAccessClient(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<oAuthPersonalAccessClients> PutoAuthPersonalAccessClient(string id, oAuthPersonalAccessClients user) => _service.PutoAuthPersonalAccessClient(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<oAuthPersonalAccessClients> DeleteoAuthPersonalAccessClient(string id) => _service.DeleteoAuthPersonalAccessClient(id);
    }
}