using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOS;
using AutoMapper;
using Core.Entity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           CreateMap<Product,ProductToReturnDto>()
            .ForMember(p=>p.ProductBrand,m=>m.MapFrom(s=>s.ProductBrand.Name))
            .ForMember(t=>t.ProductType,m=>m.MapFrom(s=>s.ProductType.Name))
            .ForMember(d=>d.PictureUrl,m=>m.MapFrom<ProductUrlResolver>()); 
        }
    }
}