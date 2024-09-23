namespace PetStoreService.Domain.Entities;

public partial class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int? ToyId { get; set; }
    public DateTime DatePosted { get; set; }
    public string Author { get; set; }

    public virtual Toy Toy { get; set; }
}