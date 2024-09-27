namespace PetStoreService.Application.Helper.Pagination
{
    public abstract class PagedResultBase
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}