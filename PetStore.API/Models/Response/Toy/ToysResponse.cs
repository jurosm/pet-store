using PetStore.API.Models.Response.Category;
using System.Collections.Generic;

namespace PetStore.API.Models.Response.Toy
{
    public class ToysResponse : PageResponse<ToyUnit>
    {
        public IEnumerable<CategoryUnit> Categories { get; set; }
    }
}
