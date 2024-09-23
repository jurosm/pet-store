namespace PetStoreService.Application.Models.Response.Order;

public class OrderListItem
{
    public DateTime OrderDate { get; set; }
    public int OrderId { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerSurname { get; set; }
    public required string ShippingAddress { get; set; }
    public required string OrderStatus { get; set; }
}