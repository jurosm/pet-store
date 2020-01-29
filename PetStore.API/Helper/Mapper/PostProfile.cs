using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Models.Request;
using PetStore.API.Models.Request.Comment;
using PetStore.API.Models.Request.Order;
using PetStore.API.Models.Request.Toy;
using PetStore.API.Models.Response;
using PetStore.API.Models.Response.Category;
using PetStore.API.Models.Response.Comment;
using PetStore.API.Models.Response.Order;
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

            CreateMap<Toy, ToyResponse>().ForMember(dest =>
            dest.CategoryId, opt =>
            opt.MapFrom(src => src.CategoryId.HasValue ? src.CategoryId : null))
                .ReverseMap();

            CreateMap<ToyData, Toy>();

            CreateMap<OrderItemRequest, OrderItem>().ReverseMap();

            CreateMap<Toy, OrderItemRequest>().ReverseMap();

            CreateMap<OrderRequest, Order>().ForMember(dest =>
            dest.ShippingAddress, opt =>
            opt.MapFrom(src => 
                src.Country + "," + src.City + "," + src.StreetAddress));

            CreateMap<Order, OrderListItem>().ReverseMap();

            CreateMap<Category, CategoryUnit>().ReverseMap();

            CreateMap<Comment, CommentsUnit>().ReverseMap();

            CreateMap<CommentData, Comment>().ReverseMap();
        }
    }

}
