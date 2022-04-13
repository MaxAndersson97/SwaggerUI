using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RelationalSchemaController
    {
        private RelationalSchemaService _service;
        public RelationalSchemaController(RelationalSchemaService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<RelationalSchema>> GetRelationalSchemas() => _service.GetRelationalSchemas();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<RelationalSchema> GetRelationalSchema(string id) => _service.GetRelationalSchema(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<RelationalSchema> PostRelationalSchema(RelationalSchema user) => _service.PostRelationalSchema(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<RelationalSchema> PutRelationalSchema(string id, RelationalSchema user) => _service.PutRelationalSchema(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<RelationalSchema> DeleteRelationalSchema(string id) => _service.DeleteRelationalSchema(id);
    }
}