using Newtonsoft.Json;
using PetStoreService.Application.Models.Request.Auth;
using PetStoreService.Web.Tests.Mocks;
using System.Text;

namespace PetStoreService.Web.Tests.Integration;

public class AuthTests : TestBase
{
    [Fact]
    public async void Login_ValidUserCredentials_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var validReq = MockIdentityManager.ValidRequest;

        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(validReq), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.OK, loginRes.StatusCode);
    }

    [Fact]
    public async void Login_InvalidUserCredentials_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var invalidReq = MockIdentityManager.InvalidRequest;
        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(invalidReq), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, loginRes.StatusCode);
    }
}