using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using _NetCore.DTO;

namespace Swagger.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpPost("[action]")]
        public ActionResult<GetProducts> GetProducts([FromBody] Pagination Pagination) => _service.GetProducts(Pagination);

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Products> GetProduct(string id) => _service.GetProduct(id);

        //[Authorize]
        [HttpPost("/Products/Filter")]
        public ActionResult<List<Products>> GetFilterProducts(string code, string product, string category, string brand) => _service.GetFilterProducts(code, product, category, brand);

        //[Authorize]
        [HttpPost]
        public ActionResult<Products> PostProduct(Products user) => _service.PostProduct(user);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Products> PutProduct(string id, UpdateProduct user) => _service.PutProduct(id, user);

        //[Authorize]
        //[AllowAnonymous]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Products> DeleteProduct(string id) => _service.DeleteProduct(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleProducts([FromBody]string[] ids) => _service.DeleteMultipleProducts(ids);
    }
}