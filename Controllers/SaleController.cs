using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController
    {
        private SaleService _service;
        public SaleController(SaleService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<Sales>> GetSales() => _service.GetSales();

        [Authorize]
        [HttpGet("{id:length(24)}")]
        public ActionResult<Sales> GetSale(string id) => _service.GetSale(id);

        //[Authorize]
        [HttpGet("WareHouse/{id:length(24)}")]
        public ActionResult<List<Sales>> GetWareHouseSale(string warehouse_id) => _service.GetWareHouseSale(warehouse_id);
        //[Authorize]
        [HttpPost]
        public ActionResult<Sales> PostSale(Sales user) => _service.PostSale(user);

        //[Authorize]
        [HttpPost("Sales/Filter")]
        public ActionResult<List<Sales>> GetSalesByFilter(string date, string reference, string customer, string sale, string paymentChoice) => _service.GetSalesByFilter(date, reference, customer, sale, paymentChoice);

        //[Authorize]
        [HttpPost("Filter")]
        public ActionResult<List<Sales>> GetSalesByFilter(string date, string reference, string customer, string warehouse_id, string statut, string paymentStatus) => _service.GetSalesByFilter(date, reference, customer, warehouse_id, statut, paymentStatus);


        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public ActionResult<Sales> PutSale(string id, Sales user) => _service.PutSale(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public ActionResult<Sales> DeleteSale(string id) => _service.DeleteSale(id);

        [HttpPost("[action]")]
        public ActionResult<bool> DeleteMultipleSale([FromBody] string[] ids) => _service.DeleteMultipleSale(ids);
    }
}