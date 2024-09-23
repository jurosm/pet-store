using Newtonsoft.Json;
using PetStoreService.Domain.Entities;
using PetStoreService.Web.Tests.Helper;
using PetStoreService.Web.Tests.Integration.Fixtures;
using System.Text;

namespace PetStoreService.Web.Tests.Integration;

public class ToyTests : TestBase
{
    [Fact]
    public async void CreateToy_ValidToy_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createToyRes = await client.PostAsync("/toy", new StringContent(JsonConvert.SerializeObject(ToyFixture.Generate()), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Created, createToyRes.StatusCode);
    }

    [Fact]
    public async void UpdateToy_ValidToy_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createToyRes = await client.PostAsync("/toy", new StringContent(JsonConvert.SerializeObject(ToyFixture.Generate()), Encoding.UTF8, "application/json"));

        var toyResBodyString = createToyRes.Content.ReadAsStringAsync();
        var toyResBody = JsonConvert.DeserializeObject<Toy>(toyResBodyString.Result);

        var toyId = toyResBody!.Id;

        var updateToyRes = await client.PutAsync($"/toy/{toyId}", new StringContent(JsonConvert.SerializeObject(ToyFixture.Generate()), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.OK, updateToyRes.StatusCode);
    }

    [Fact]
    public async void DeleteToy_ValidToy_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createToyRes = await client.PostAsync("/toy", new StringContent(JsonConvert.SerializeObject(ToyFixture.Generate()), Encoding.UTF8, "application/json"));

        var createToyResBodyString = createToyRes.Content.ReadAsStringAsync();
        var toyResBody = JsonConvert.DeserializeObject<Toy>(createToyResBodyString.Result);

        var toyId = toyResBody!.Id;

        var deleteToyRes = await client.DeleteAsync($"/toy/{toyId}");

        Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteToyRes.StatusCode);
    }

    [Fact]
    public async void FetchToys_NoQuery_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var getToysRes = await client.GetAsync($"/toy");

        Assert.Equal(System.Net.HttpStatusCode.OK, getToysRes.StatusCode);
    }

    [Fact]
    public async void FetchSingleToy_ValidToyId_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var toy = await ToyHelper.CreateToy(client);

        var getToyRes = await client.GetAsync($"/toy/{toy.Id}");

        Assert.Equal(System.Net.HttpStatusCode.OK, getToyRes.StatusCode);
    }
}