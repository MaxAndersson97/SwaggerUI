using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController
    {
        private RoleService _service;
        public RoleController(RoleService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Roles>> GetRoles() => _service.GetRoles();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Roles> GetRole(string id) => _service.GetRole(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Roles> PostRole(Roles user) => _service.PostRole(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Roles> PutRole(string id, Roles user) => _service.PutRole(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Roles> DeleteRole(string id) => _service.DeleteRole(id);
    }
}