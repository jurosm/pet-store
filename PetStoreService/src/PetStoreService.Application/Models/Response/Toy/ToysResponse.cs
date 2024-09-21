using PetStoreService.Application.Models.Response.Category;

namespace PetStoreService.Application.Models.Response.Toy;

public class ToysResponse : PageResponse<ToyUnit>
{
    public IEnumerable<CategoryUnit> Categories { get; set; }
}
