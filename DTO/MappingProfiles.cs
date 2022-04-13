using AutoMapper;
using Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _NetCore.DTO
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<UpdateProduct, Products>()
                 .ForMember(dest => dest.product_Images, act => act.Ignore());
        }
    }
}
