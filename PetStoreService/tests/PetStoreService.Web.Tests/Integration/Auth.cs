using Newtonsoft.Json;
using PetStoreService.Application.Models.Request.Auth;
using System.Text;

namespace PetStoreService.Web.Tests.Integration;

public class AuthTests : TestBase
{
    [Fact]
    public async void Login_ValidUserCredentials_ReturnsOk()
    {
        var client = _factory.CreateClient();

        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            Email = EnvironmentVariables.Auth0Username,
            Password = EnvironmentVariables.Auth0Password
        }), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.OK, loginRes.StatusCode);
    }

    [Fact]
    public async void Login_InvalidUserCredentials_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            Email = "hehe@gmail.com",
            Password = EnvironmentVariables.Auth0Password
        }), Encoding.UTF8, "application/json"));

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, loginRes.StatusCode);
    }
}