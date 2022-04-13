using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class oAuthRefreshTokenController
    {
        private oAuthRefreshTokenService _service;
        public oAuthRefreshTokenController(oAuthRefreshTokenService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<oAuthRefreshTokens>> GetoAuthRefreshTokens() => _service.GetoAuthRefreshTokens();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<oAuthRefreshTokens> GetoAuthRefreshToken(string id) => _service.GetoAuthRefreshToken(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<oAuthRefreshTokens> PostoAuthRefreshToken(oAuthRefreshTokens user) => _service.PostoAuthRefreshToken(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<oAuthRefreshTokens> PutoAuthRefreshToken(string id, oAuthRefreshTokens user) => _service.PutoAuthRefreshToken(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<oAuthRefreshTokens> DeleteoAuthRefreshToken(string id) => _service.DeleteoAuthRefreshToken(id);
    }
}