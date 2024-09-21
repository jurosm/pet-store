using Newtonsoft.Json;
using PetStoreService.Application.Models.Request.Auth;
using PetStoreService.Application.Models.Response.Auth;
using System.Net.Http.Headers;
using System.Text;

namespace PetStoreService.Web.Tests.Integration.Helper;

public static class Auth
{
    public static async Task<AuthenticationHeaderValue> Login(HttpClient client)
    {
        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            Email = EnvironmentVariables.Auth0Username,
            Password = EnvironmentVariables.Auth0Password
        }), Encoding.UTF8, "application/json"));

        if(loginRes.StatusCode != System.Net.HttpStatusCode.OK) {
            throw new Exception("Login failed");
        }

        var loginResBodyString = loginRes.Content.ReadAsStringAsync();
        var loginResBody = JsonConvert.DeserializeObject<LoginResponse>(loginResBodyString.Result);

        return new AuthenticationHeaderValue("Bearer", loginResBody!.JwtToken);
    }
}