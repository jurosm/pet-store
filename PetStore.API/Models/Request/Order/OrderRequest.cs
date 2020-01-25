using PetStore.API.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Order
{
    public class OrderRequest
    {
        public List<OrderItemRequest> OrderItems { get; set; }
        public string TokenId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
    }
}
