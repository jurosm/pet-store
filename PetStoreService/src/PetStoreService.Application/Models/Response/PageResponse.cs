namespace PetStoreService.Application.Models.Response;

public class PageResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int NumberOfPages { get; set; }

    public PageResponse()
    {
        Items = new List<T>();
    }
}
