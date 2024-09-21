using Newtonsoft.Json;
using PetStoreService.Domain.Entities;
using PetStoreService.Web.Tests.Integration.Fixtures;
using System.Text;

namespace PetStoreService.Web.Tests.Integration.Helper;

public static class ToyHelper
{
    public static async Task<Toy> CreateToy(HttpClient client)
    {
        var createToyRes = await client.PostAsync("/toy", new StringContent(JsonConvert.SerializeObject(ToyFixture.Generate()), Encoding.UTF8, "application/json"));

        var toyResBodyString = createToyRes.Content.ReadAsStringAsync();
        var toyResBody = JsonConvert.DeserializeObject<Toy>(toyResBodyString.Result);

        return toyResBody;
    }
}