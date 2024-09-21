using PetStoreService.Application.Models.Response.Category;
using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Response.Toy;

public class ToyChangeRequest
{
    [Required]
    public string ShortDescription { get; set; }
    [Required]
    public string Description { get; set; }
    public CategoryUnit Category { get; set; }
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
}