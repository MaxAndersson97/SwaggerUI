using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencieController
    {
        private CurrencieService _service;
        public CurrencieController(CurrencieService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Currencies>> GetCurrencies() => _service.GetCurrencies();
        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Currencies> GetCurrency(string id) => _service.GetCurrency(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Currencies> PostCurrency(Currencies user) => _service.PostCurrency(user);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Currencies> PutCurrency(string id, Currencies user) => _service.PutCurrency(id, user);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Currencies> DeleteCurrency(string id) => _service.DeleteCurrency(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleCurrency([FromBody] string[] ids) => _service.DeleteMultipleCurrency(ids);
    }
}