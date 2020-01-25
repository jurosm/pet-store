using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Db;
using PetStore.API.Models.Request.Order;
using PetStore.API.Services.CRUD;

namespace PetStore.API.Services.OrderSystem
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ContextWrapper<Order> context) : base(context) { }

        public bool CheckValidOrder(List<OrderItemRequest> orderItems)
        {
            return orderItems.All(x => ((Context.PSContext.Toy.ToList().Any(y => ((y.ToyId == x.ToyId) && (y.Quantity >= x.Quantity))))));
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
