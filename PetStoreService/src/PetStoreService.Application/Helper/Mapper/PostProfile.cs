using AutoMapper;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Request.Order;
using PetStoreService.Application.Models.Request.Toy;
using PetStoreService.Application.Models.Response.Category;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Application.Models.Response.Order;
using PetStoreService.Application.Models.Response.Toy;
using PetStoreService.Application.Models.Services.Order;
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
                src.Country + "," + src.City + "," + src.StreetAddress))
                .ForMember(dest => dest.OrderItem,
                opt => opt.MapFrom(src => src.OrderItem.Select(oir => new OrderItem() { ToyId = oir.ToyId, Quantity = oir.Quantity }).ToList()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => OrderStatus.Draft));

            CreateMap<Order, OrderListItem>().ReverseMap();
            CreateMap<Order, GetOrderResponse>();

            CreateMap<Category, CategoryUnit>().ReverseMap();

            CreateMap<Comment, CommentsUnit>().ReverseMap();

            CreateMap<CommentData, Comment>().ReverseMap();

            CreateMap<Toy, CreateOrderToyResponse>().ReverseMap();
            CreateMap<CreateOrderItemResponse, OrderItem>().ForMember(src => src.Toy, opt => opt.MapFrom(dest => dest.Toy)).ReverseMap();
            CreateMap<Order, CreateOrderResponse>().ForMember(src => src.OrderItem, opt => opt.MapFrom(dest => dest.OrderItem)).ReverseMap();
        }
    }
}