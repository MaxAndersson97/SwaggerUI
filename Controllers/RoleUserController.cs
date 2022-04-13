using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RoleUserController
    {
        private RoleUserService _service;
        public RoleUserController(RoleUserService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<RoleUsers>> GetRoleUsers() => _service.GetRoleUsers();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<RoleUsers> GetRoleUser(string id) => _service.GetRoleUser(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<RoleUsers> PostRoleUser(RoleUsers user) => _service.PostRoleUser(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<RoleUsers> PutRoleUser(string id, RoleUsers user) => _service.PutRoleUser(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<RoleUsers> DeleteRoleUser(string id) => _service.DeleteRoleUser(id);
    }
}