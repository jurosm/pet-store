using PetStoreService.Domain.Entities;

namespace PetStoreService.Web.Tests.Integration.Fixtures;

public class CategoryFixture
{
    public static Category Generate()
    {
        return new Category
        {
            Name = new Bogus.Faker().Name.FullName()
        };
    }
}