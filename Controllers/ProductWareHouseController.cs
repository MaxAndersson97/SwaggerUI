using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductWareHouseController
    {
        private ProductWareHouseService _service;
        public ProductWareHouseController(ProductWareHouseService service)
        {
            _service = service;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<ProductWareHouse>> GetProductWareHouses() => _service.GetProductWareHouses();
        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<ProductWareHouse> GetProductWareHouse(string id) => _service.GetProductWareHouse(id);
        //[Authorize]
        [HttpPost]
        public ActionResult<ProductWareHouse> PostProductWareHouse(ProductWareHouse user) => _service.PostProductWareHouse(user);
        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<ProductWareHouse> PutProductWareHouse(string id, ProductWareHouse user) => _service.PutProductWareHouse(id, user);
        //[Authorize]
        [HttpPost("warehouse")]
        public ActionResult<List<ProductWareHouse>> getProductByWareHouse(string warehouseId) => _service.getProductByWareHouse(warehouseId);
        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<ProductWareHouse> DeleteProductWareHouse(string id) => _service.DeleteProductWareHouse(id);
    }
}