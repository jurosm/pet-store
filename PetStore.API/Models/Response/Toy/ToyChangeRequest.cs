using PetStore.API.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Toy
{
    public class ToyChangeRequest
    {
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        public CategoryUnit Category { get; set; }
        public int ToyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
