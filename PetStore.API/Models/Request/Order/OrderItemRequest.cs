using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Order
{
    public class OrderItemRequest
    {
        public int Quantity { get; set; }
        public int ToyId { get; set; }
    }
}
