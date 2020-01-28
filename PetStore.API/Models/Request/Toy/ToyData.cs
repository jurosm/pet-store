using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Request.Toy
{
    public class ToyData
    {
        [Required]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
