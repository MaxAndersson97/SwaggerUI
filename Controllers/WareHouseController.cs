using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class WareHouseController
    {
        private WareHouseService _service;
        public WareHouseController(WareHouseService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<WareHouses>> GetWareHouses() => _service.GetWareHouses();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<WareHouses> GetWareHouse(string id) => _service.GetWareHouse(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<WareHouses> PostWareHouse(WareHouses user) => _service.PostWareHouse(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<WareHouses> PutWareHouse(string id, WareHouses user) => _service.PutWareHouse(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<WareHouses> DeleteWareHouse(string id) => _service.DeleteWareHouse(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleWareHouse([FromBody] string[] ids) => _service.DeleteMultipleWareHouse(ids);
    }
}