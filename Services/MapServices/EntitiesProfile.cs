using AutoMapper;
using Domain.EntitesDto;
using Domain.Entities;
using Services.EntitiesServices.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapServices
{
    public  class EntitiesProfile:Profile
    {
        public EntitiesProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
