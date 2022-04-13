using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SettingController
    {
        private SettingService _service;
        public SettingController(SettingService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Settings>> GetSettings() => _service.GetSettings();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Settings> GetSetting(string id) => _service.GetSetting(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Settings> PostSetting(Settings user) => _service.PostSetting(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Settings> PutSetting(string id, Settings user) => _service.PutSetting(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Settings> DeleteSetting(string id) => _service.DeleteSetting(id);
    }
}