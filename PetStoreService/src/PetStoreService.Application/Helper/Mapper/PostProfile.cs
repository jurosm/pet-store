using AutoMapper;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Response.Toy;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Helper.Mapper
{
    public class PostProfile : Profile
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