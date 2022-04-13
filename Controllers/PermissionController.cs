using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController
    {
        private PermissionService _service;
        public PermissionController(PermissionService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Permissions>> GetPermissions() => _service.GetPermissions();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Permissions> GetPermission(string id) => _service.GetPermission(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Permissions> PostPermission(Permissions user) => _service.PostPermission(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Permissions> PutPermission(string id, Permissions user) => _service.PutPermission(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Permissions> DeletePermission(string id) => _service.DeletePermission(id);
    }
}