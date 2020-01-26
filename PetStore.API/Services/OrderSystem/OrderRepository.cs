using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Db;
using PetStore.API.Models.Request.Order;
using PetStore.API.Services.CRUD;

namespace PetStore.API.Services.OrderSystem
{
    public class OrderRepository : Repository<Order>
    {
        readonly IMapper Mapper;
        public OrderRepository(ContextWrapper<Order> context, IMapper mapper) : base(context) {
            this.Mapper = mapper;
        }

        public bool CheckValidOrder(List<OrderItemRequest> orderItems)
        {
            List<OrderItemRequest> toys = Context.PSContext.Toy.Select(x => Mapper.Map<OrderItemRequest>(x)).ToList();
            return orderItems.All(x => ((toys.Any(y => ((y.ToyId == x.ToyId) && (y.Quantity >= x.Quantity))))));
        }

        public decimal PricePerOrder(List<OrderItemRequest> orderItems)
        {
            return orderItems.Sum(x => Context.PSContext.Toy.ToList().Find(y => y.ToyId == x.ToyId).Price*x.Quantity);
        }

        public async Task RemoveItemsAsync(List<OrderItemRequest> orderItems)
        {
            orderItems.ForEach(async x => 
            {
                Toy toy = (await Context.PSContext.Toy.FindAsync(x.ToyId));
                toy.Quantity -= x.Quantity;
            });
            await Context.SaveChangesAsync();
        }
    }
}
