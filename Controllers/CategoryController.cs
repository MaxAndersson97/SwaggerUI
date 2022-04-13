using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController
    {
        private CategoryService _service;
        public CategoryController(CategoryService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Categories>> GetCategories() => _service.GetCategories();
        [HttpGet("{id:length(24)}")]
        public ActionResult<Categories> GetCategory(string id) => _service.GetCategory(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Categories> PostCategory(Categories Categories) => _service.PostCategory(Categories);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Categories> PutCategory([FromBody] Categories Categories) => _service.PutCategory(Categories);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Categories> DeleteCategory(string id) => _service.DeleteCategory(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleCategory([FromBody] string[] ids) => _service.DeleteMultipleCategory(ids);
    }
}