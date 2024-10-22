namespace PetStoreService.Application.Models.Services.Order;

public class CreateOrderItemResponse
{
    public int Quantity { get; set; }
    public int Id { get; set; }
    public required CreateOrderToyResponse Toy { get; set; }
    public int ToyId { get; set; }
}
