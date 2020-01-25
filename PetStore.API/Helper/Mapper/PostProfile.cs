using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Models.Request;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Response;
using PetStore.API.Models.Response.Toy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Helper.Mapper
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Toy, ToyUnit>().ForMember(dest => 
            dest.Category, opt => 
            opt.MapFrom(src => src.CategoryId.HasValue ? src.Category.Name : null))
                .ReverseMap();

            CreateMap<OrderItemRequest, OrderItem>().ReverseMap();

            CreateMap<OrderRequest, Order>().ForMember(dest =>
            dest.ShippingAddress, opt =>
            opt.MapFrom(src => 
                src.Country + "," + src.City + "," + src.StreetAddress));
        }
    }

}
