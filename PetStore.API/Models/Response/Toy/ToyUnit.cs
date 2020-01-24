using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Toy
{
    public class ToyUnit
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string Category { get; set; }
        public int ToyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
