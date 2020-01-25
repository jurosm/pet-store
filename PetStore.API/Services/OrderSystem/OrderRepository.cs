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
            return Context.PSContext.Toy.ToList().All(x => (!(orderItems.Any(y => ((y.ToyId == x.ToyId) && (y.Quantity > x.Quantity)))))
            && orderItems.Any(y => x.ToyId == y.ToyId));
        }

        public decimal PricePerOrder(List<OrderItemRequest> orderItems)
        {
            return Context.PSContext.Toy.ToList().Sum(x =>  orderItems.Find(y => y.ToyId == x.ToyId).Quantity*x.Price);
        }
    }
}
