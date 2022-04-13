using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionRoleController
    {
        private PermissionRoleService _userService;
        public PermissionRoleController(PermissionRoleService userService)
        {
            _userService = userService;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PermissionRoles>> GetPermissionRoles() => _userService.GetPermissionRoles();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PermissionRoles> GetPermissionRole(string id) => _userService.GetPermissionRole(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PermissionRoles> PostPermissionRole(PermissionRoles user) => _userService.PostPermissionRole(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PermissionRoles> PutPermissionRole(string id, PermissionRoles user) => _userService.PutPermissionRole(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PermissionRoles> DeletePermissionRole(string id) => _userService.DeletePermissionRole(id);
    }
}