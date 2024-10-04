namespace PetStoreService.Application.Models.Services.Order;

public class CreateOrderResponse
{
    public DateTime OrderDate { get; set; }
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerSurname { get; set; }
    public string ShippingAddress { get; set; }
    public string IpinfoAddress { get; set; }
    public string OrderStatus { get; set; }
    public string? ExternalReferenceId { get; set; }
    public decimal Total {get; set;}

    public virtual List<CreateOrderItemResponse> OrderItem { get; set; }
}