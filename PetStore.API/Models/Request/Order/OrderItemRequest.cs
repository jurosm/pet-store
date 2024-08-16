namespace PetStore.API.Models.Request.Order
{
    public class OrderItemRequest
    {
        public int Quantity { get; set; }
        public int ToyId { get; set; }
    }
}
