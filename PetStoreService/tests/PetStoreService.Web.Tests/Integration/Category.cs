using Newtonsoft.Json;
using PetStoreService.Domain.Entities;
using PetStoreService.Web.Tests.Helper;
using PetStoreService.Web.Tests.Integration.Fixtures;
using System.Text;

namespace PetStoreService.Web.Tests.Integration;

public class CategoryTests : TestBase
{
    [Fact]
    public async void CreateCategory_ValidCategory_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createCategoryRes = await client.PostAsync("/category", new StringContent(JsonConvert.SerializeObject(CategoryFixture.Generate()), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Created, createCategoryRes.StatusCode);
    }

    [Fact]
    public async void UpdateCategory_ValidCategory_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createCategoryRes = await client.PostAsync("/category", new StringContent(JsonConvert.SerializeObject(CategoryFixture.Generate()), Encoding.UTF8, "application/json"));

        var categoryResBodyString = createCategoryRes.Content.ReadAsStringAsync();
        var categoryResBody = JsonConvert.DeserializeObject<Category>(categoryResBodyString.Result);

        var categoryId = categoryResBody!.Id;

        var updateCategoryRes = await client.PostAsync($"/category/{categoryId}", new StringContent(JsonConvert.SerializeObject(CategoryFixture.Generate()), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.OK, updateCategoryRes.StatusCode);
    }

    [Fact]
    public async void DeleteCategory_ValidCategory_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var createCategoryRes = await client.PostAsync("/category", new StringContent(JsonConvert.SerializeObject(CategoryFixture.Generate()), Encoding.UTF8, "application/json"));

        var createCategoryResBodyString = createCategoryRes.Content.ReadAsStringAsync();
        var categoryResBody = JsonConvert.DeserializeObject<Category>(createCategoryResBodyString.Result);

        var categoryId = categoryResBody!.Id;

        var deleteCategoryRes = await client.DeleteAsync($"/category/{categoryId}");

        Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteCategoryRes.StatusCode);
    }

    [Fact]
    public async void GetAllCategories_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var authHeader = await Auth.Login(client);

        client.DefaultRequestHeaders.Authorization = authHeader;

        var categoryRes = await client.GetAsync("/category");

        Assert.Equal(System.Net.HttpStatusCode.OK, categoryRes.StatusCode);
    }
}