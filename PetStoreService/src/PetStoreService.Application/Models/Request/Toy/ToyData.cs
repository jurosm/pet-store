using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Request.Toy;

public class ToyData
{
    [Required]
    public required string Description { get; set; }
    public int? CategoryId { get; set; }
    [Required]
    [MaxLength(30, ErrorMessage = "Maximum length is 30")]
    public required string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "Maximum length is 50")]
    public required string ShortDescription { get; set; }
    [Required]
    public int Quantity { get; set; }
}