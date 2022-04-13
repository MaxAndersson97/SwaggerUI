using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController
    {
        private ClientService _service;
        public ClientController(ClientService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Clients>> GetClients() => _service.GetClients();
        [HttpGet("{id:length(24)}")]
        public ActionResult<Clients> GetClient(string id) => _service.GetClient(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Clients> PostClient(Clients user) => _service.PostClient(user);
        //[Authorize]
        [HttpPost]
        [Route("clientLogin")]
        public ActionResult<Clients> LoginClient(string email, string password) => _service.LoginClient(email, password);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Clients> PutClient(string id, Clients user) => _service.PutClient(id, user);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Clients> DeleteClient(string id) => _service.DeleteClient(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleUser([FromBody] string[] ids) => _service.DeleteMultipleUser(ids);

        //[HttpPost("/Client/Filter")]
        //public ActionResult<List<Products>> GetFilterUsers(string code, string name, string phone, string email) => _service.GetFilterUsers(code, name, phone, email);
    }
}
