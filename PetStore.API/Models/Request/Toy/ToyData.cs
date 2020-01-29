using PetStore.API.Models.Response.Category;
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
        [MaxLength(30,ErrorMessage = "Maximum length is 30")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        public string ShortDescription { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
