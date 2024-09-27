namespace PetStoreService.Application.Models.Response;

public class PageResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }

    public PageResponse()
    {
        Items = [];
    }
}