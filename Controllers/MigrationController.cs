using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MigrationController
    {
        private MigrationService _service;
        public MigrationController(MigrationService service)
        {
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult<List<Migrations>> GetMigrations() => _service.GetMigrations();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Migrations> GetMigration(string id) => _service.GetMigration(id);

        //[Authorize]
        [HttpPost]
        public ActionResult<Migrations> PostMigration(Migrations migration) => _service.PostMigration(migration);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Migrations> PutMigration(string id, Migrations migration) => _service.PutMigration(id, migration);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Migrations> DeleteMigration(string id) => _service.DeleteMigration(id);
    }
}