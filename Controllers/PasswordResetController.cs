using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordResetController
    {
        private PasswordResetService _service;
        public PasswordResetController(PasswordResetService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PasswordResets>> GetPasswordResets() => _service.GetPasswordResets();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PasswordResets> GetPasswordReset(string id) => _service.GetPasswordReset(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PasswordResets> PostPasswordReset(PasswordResets PasswordResets) => _service.PostPasswordReset(PasswordResets);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PasswordResets> PutPasswordReset(string id, PasswordResets PasswordResets) => _service.PutPasswordReset(id, PasswordResets);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PasswordResets> DeletePasswordReset(string id) => _service.DeletePasswordReset(id);
    }
}