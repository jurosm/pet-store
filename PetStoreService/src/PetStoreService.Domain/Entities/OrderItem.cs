namespace PetStoreService.Domain.Entities;

public partial class OrderItem
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int? ToyId { get; set; }
    public int Id { get; set; }

    public virtual Order Order { get; set; }
    public virtual Toy Toy { get; set; }
}