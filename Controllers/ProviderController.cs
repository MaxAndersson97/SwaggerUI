using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController
    {
        private ProvidersService _service;
        public ProviderController(ProvidersService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Providers>> GetProviders() => _service.GetProviders();
        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Providers> GetProvider(string id) => _service.GetProvider(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Providers> PostProvider(Providers user) => _service.PostProvider(user);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Providers> PutProvider(string id, Providers user) => _service.PutProvider(id, user);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Providers> DeleteProvider(string id) => _service.DeleteProvider(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleSuplier([FromBody] string[] ids) => _service.DeleteMultipleSuplier(ids);
    }
}