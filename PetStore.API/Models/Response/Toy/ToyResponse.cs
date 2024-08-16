namespace PetStore.API.Models.Response.Toy
{
    public class ToyResponse
    {
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ToyId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
