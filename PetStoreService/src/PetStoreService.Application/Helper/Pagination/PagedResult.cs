using Microsoft.EntityFrameworkCore;

namespace PetStoreService.Application.Helper.Pagination;

public class PagedResult<T> : PagedResultBase where T : class
{
    public IEnumerable<T>? Results { get; set; }

    public static async Task<PagedResult<T>> GetPaged(IQueryable<T> query, int offset, int limit)
    {
        var baseQuery = query.AsQueryable();

        var result = new PagedResult<T>
        {
            Offset = offset,
            Limit = limit,
            Total = await query.CountAsync(),
            Results = await baseQuery.Skip(offset).Take(limit).ToArrayAsync()
        };

        return result;
    }
}