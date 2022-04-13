using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore.DTO
{
    public class GetAdjustments
    {

        public List<Adjustments> Adjustments { get; set; } = new List<Adjustments>();
        public long Total { get; set; }

    }
}
