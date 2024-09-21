using PetStoreService.Domain.Entities;

namespace PetStoreService.Web.Tests.Fixtures;

public class CommentFixture
{
    public static Comment Generate()
    {
        return new Comment
        {
            Author = new Bogus.Faker().Name.FullName(),
            Text = new Bogus.Faker().Lorem.Sentence()
        };
    }
}