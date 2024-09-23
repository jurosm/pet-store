using PetStoreService.Domain.Entities;

namespace PetStoreService.Web.Tests.Integration.Fixtures;

public class ToyFixture
{
    public static Toy Generate()
    {
        return new Toy
        {
            Name = new Bogus.Faker().Commerce.ProductName(),
            Description = new Bogus.Faker().Lorem.Sentence(),
            Price = decimal.Parse(new Bogus.Faker().Commerce.Price()),
            ShortDescription = new Bogus.Faker().Commerce.ProductName(),
            Quantity = new Bogus.Faker().Random.Int(1, 1000),
        };
    }
}