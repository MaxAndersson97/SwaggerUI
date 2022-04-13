using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore.DTO
{
    public class GetProducts
    {
        public List<Products> Products { get; set; } = new List<Products>();
        public long Total { get; set; }
    }
}
