using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UnitController
    {
        private UnitService _service;
        public UnitController(UnitService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Units>> GetUnits() => _service.GetUnits();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Units> GetUnit(string id) => _service.GetUnit(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Units> PostUnit(Units user) => _service.PostUnit(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Units> PutUnit(string id, Units user) => _service.PutUnit(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Units> DeleteUnit(string id) => _service.DeleteUnit(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleUnit([FromBody] string[] ids) => _service.DeleteMultipleUnit(ids);
    }
}