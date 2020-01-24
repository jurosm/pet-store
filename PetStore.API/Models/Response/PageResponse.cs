using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Models.Response
{
    public class PageResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int NumberOfPages { get; set; }

        public PageResponse()
        {
            this.Items = new List<T>();
        }
    }
}
