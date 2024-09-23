using Microsoft.EntityFrameworkCore;

namespace PetStoreService.Application.Helper.Pagination;

public class PagedResult<T> : PagedResultBase where T : class
{
    public IEnumerable<T>? Results { get; set; }

    public static async Task<PagedResult<T>> GetPaged(IQueryable<T> query,
                          int page, int pageSize)
    {
        if (pageSize <= 0 || page <= 0) { pageSize = 6; page = 1; }

        var baseQuery = query.AsQueryable();

        var result = new PagedResult<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = await query.CountAsync()
        };

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;

        if (skip + pageSize <= result.RowCount)
        {
            result.Results = await baseQuery.Skip(skip).Take(pageSize).ToListAsync();
        }
        else if (skip + pageSize > result.RowCount && skip < result.RowCount)
        {
            result.Results = await baseQuery.Skip(skip).ToListAsync();
        }

        return result;
    }
}