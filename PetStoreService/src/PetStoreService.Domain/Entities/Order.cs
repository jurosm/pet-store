using System;
using System.Collections.Generic;

namespace PetStoreService.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = [];
        }

        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string ShippingAddress { get; set; }
        public string IpinfoAddress { get; set; }
        public string OrderStatus { get; set; }
        public string ExternalReferenceId { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}