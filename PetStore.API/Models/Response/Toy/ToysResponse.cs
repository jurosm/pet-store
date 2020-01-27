using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response.Toy
{
    public class ToysResponse : PageResponse<ToyUnit> 
    {
        public string[] Categories { get; set; }
    }
}
