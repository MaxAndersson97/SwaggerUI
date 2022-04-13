using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController
    {
        private ServerService _service;
        public ServerController(ServerService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Servers>> GetServers() => _service.GetServers();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Servers> GetServer(string id) => _service.GetServer(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Servers> PostServer(Servers user) => _service.PostServer(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Servers> PutServer(string id, Servers user) => _service.PutServer(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Servers> DeleteServer(string id) => _service.DeleteServer(id);
    }
}