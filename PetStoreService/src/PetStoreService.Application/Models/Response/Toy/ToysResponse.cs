using PetStoreService.Application.Models.Response.Category;

namespace PetStoreService.Application.Models.Response.Toy;

public class ToysResponse : PageResponse<ToyUnit>
{
    public required IEnumerable<CategoryUnit> Categories { get; set; }
}