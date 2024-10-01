public class GetOrderResponse
{
    public DateTime OrderDate { get; set; }
    public int Id { get; set; }
    public required string CustomerName { get; set; }
    public required string CustomerSurname { get; set; }
    public required string ShippingAddress { get; set; }
    public required string OrderStatus { get; set; }
}