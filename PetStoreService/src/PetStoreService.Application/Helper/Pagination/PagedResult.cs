namespace PetStoreService.Application.Helper.Pagination;

public class PagedResult<T> : PagedResultBase where T : class
{
    public IEnumerable<T> Results { get; set; }

    public PagedResult() { }

    public static PagedResult<T> GetPaged(IQueryable<T> query,
                          int page, int pageSize)
    {
        if (pageSize <= 0 || page <= 0) { pageSize = 6; page = 1; }

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
            result.Results = [.. query.Skip(skip).Take(pageSize)];
        }
        else if (skip + pageSize > result.RowCount && skip < result.RowCount)
        {
            result.Results = [.. query.Skip(skip)];
        }

        return result;
    }
}