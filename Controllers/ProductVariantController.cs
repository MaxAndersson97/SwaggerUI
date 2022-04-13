using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductVariantController
    {
        private ProductVariantService _service;
        public ProductVariantController(ProductVariantService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<ProductVariants>> GetProductVariants() => _service.GetProductVariants();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<ProductVariants> GetProductVariant(string id) => _service.GetProductVariant(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<ProductVariants> PostProductVariant(ProductVariants user) => _service.PostProductVariant(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<ProductVariants> PutProductVariant(string id, ProductVariants user) => _service.PutProductVariant(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<ProductVariants> DeleteProductVariant(string id) => _service.DeleteProductVariant(id);
    }
}