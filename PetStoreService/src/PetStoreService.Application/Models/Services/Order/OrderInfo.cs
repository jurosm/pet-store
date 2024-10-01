namespace PetStoreService.Application.Models.Services.Order;

public class OrderInfo
{
    public required CreateOrderResponse Order { get; set; }
    public required string PaymentSecret { get; set; }
}

