using PetStore.API.Models.Response.Category;
using System.ComponentModel.DataAnnotations;

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