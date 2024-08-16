using System.Collections.Generic;

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
