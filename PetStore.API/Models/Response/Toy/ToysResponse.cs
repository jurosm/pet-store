using PetStore.API.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Toy
{
    public class ToysResponse : PageResponse<ToyUnit> 
    {
        public IEnumerable<CategoryUnit> Categories { get; set; }
    }
}
