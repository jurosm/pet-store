using System;
using System.Collections.Generic;

namespace PetStore.API.Db
{
    public partial class Toy
    {
        public Toy()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int ToyId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public int Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
