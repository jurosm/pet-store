using System;

namespace PetStore.API.Models.Response.Order
{
    public class OrderListItem
    {
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
    }
}