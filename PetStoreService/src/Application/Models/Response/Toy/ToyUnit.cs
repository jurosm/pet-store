using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Response.Toy;

public class ToyUnit
{
    [Required]
    public string ShortDescription { get; set; }
    public string Category { get; set; }
    public int ToyId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}
