using PetStore.API.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Toy
{
    public class ToyResponse
    {
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public CategoryUnit Category { get; set; }
        public int ToyId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
