using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Helper.Pagination
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IEnumerable<T> Results { get; set; }

        public PagedResult() {}

        public static PagedResult<T> GetPaged<T>(IQueryable<T> query,
                              int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            if (skip + pageSize <= result.RowCount)
            {
                result.Results = query.Skip(skip).Take(pageSize).ToList();
            } 
            else if(skip + pageSize > result.RowCount && skip < result.RowCount)
            {
                result.Results = query.Skip(skip).ToList();
            }

            return result;
        }
    }
}
