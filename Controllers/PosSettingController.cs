using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PosSettingController
    {
        private PosSettingService _service;
        public PosSettingController(PosSettingService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<PosSettings>> GetPosSettings() => _service.GetPosSettings();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<PosSettings> GetPosSetting(string id) => _service.GetPosSetting(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<PosSettings> PostPosSetting(PosSettings user) => _service.PostPosSetting(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<PosSettings> PutPosSetting(string id, PosSettings user) => _service.PutPosSetting(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<PosSettings> DeletePosSetting(string id) => _service.DeletePosSetting(id);
    }
}