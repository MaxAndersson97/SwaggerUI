using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class oAuthAccessCodeController
    {
        private oAuthAccessCodeService _service;
        public oAuthAccessCodeController(oAuthAccessCodeService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<oAuthAccessCodes>> GetoAuthAccessCodes() => _service.GetoAuthAccessCodes();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<oAuthAccessCodes> GetoAuthAccessCode(string id) => _service.GetoAuthAccessCode(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<oAuthAccessCodes> PostoAuthAccessCode(oAuthAccessCodes user) => _service.PostoAuthAccessCode(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<oAuthAccessCodes> PutoAuthAccessCode(string id, oAuthAccessCodes user) => _service.PutoAuthAccessCode(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<oAuthAccessCodes> DeleteoAuthAccessCode(string id) => _service.DeleteoAuthAccessCode(id);
    }
}