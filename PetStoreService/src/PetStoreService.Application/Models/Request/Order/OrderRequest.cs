using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Request.Order;

public class OrderRequest
{
    [Required]
    public required List<OrderItemRequest> OrderItems { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Maximum length is 30")]
    public required string CustomerName { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "Maximum length is 30")]
    public required string CustomerSurname { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Maximum length is 50")]
    public required string Country { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Maximum length is 50")]
    public required string City { get; set; }

    [Required]
    [MaxLength(80, ErrorMessage = "Maximum length is 80")]
    public required string StreetAddress { get; set; }
}