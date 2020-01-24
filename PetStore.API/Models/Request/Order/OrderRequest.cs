using PetStore.API.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Order
{
    public class OrderRequest
    {
        public List<OrderItem> OrderItems { get; set; }
        public string TokenId { get; set; }
    }
}
