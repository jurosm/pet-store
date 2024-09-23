namespace PetStoreService.Domain.Entities;

public partial class Toy
{
    public Toy()
    {
        Comment = [];
        OrderItem = [];
    }

    public string Description { get; set; }
    public int? CategoryId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ShortDescription { get; set; }
    public int Quantity { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<Comment> Comment { get; set; }
    public virtual ICollection<OrderItem> OrderItem { get; set; }
}