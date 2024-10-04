namespace PetStoreService.Application.Models.Services.Order;

public class CreateOrderToyResponse
{
    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ShortDescription { get; set; }
    public int Quantity { get; set; }
}