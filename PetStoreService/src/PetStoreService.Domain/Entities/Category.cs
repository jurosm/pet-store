namespace PetStoreService.Domain.Entities;

public partial class Category
{
    public Category()
    {
        Toy = [];
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Toy> Toy { get; set; }
}
