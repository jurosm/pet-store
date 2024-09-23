using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Response.Toy;

public class ToyUnit
{
    [Required]
    public required string ShortDescription { get; set; }
    public required string Category { get; set; }
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}