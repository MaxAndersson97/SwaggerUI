using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class oAuthAccessTokenController
    {
        private oAuthAccessTokenService _service;
        public oAuthAccessTokenController(oAuthAccessTokenService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<oAuthAccessTokens>> GetoAuthAccessTokens() => _service.GetoAuthAccessTokens();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<oAuthAccessTokens> GetoAuthAccessToken(string id) => _service.GetoAuthAccessToken(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<oAuthAccessTokens> PostoAuthAccessToken(oAuthAccessTokens oAuthAccessToken) => _service.PostoAuthAccessToken(oAuthAccessToken);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<oAuthAccessTokens> PutoAuthAccessToken(string id, oAuthAccessTokens oAuthAccessToken) => _service.PutoAuthAccessToken(id, oAuthAccessToken);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<oAuthAccessTokens> DeleteoAuthAccessToken(string id) => _service.DeleteoAuthAccessToken(id);
    }
}