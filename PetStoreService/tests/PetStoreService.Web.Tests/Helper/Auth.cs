using Newtonsoft.Json;
using PetStoreService.Application.Models.Request.Auth;
using PetStoreService.Application.Models.Response.Auth;
using PetStoreService.Web.Tests.Mocks;
using System.Net.Http.Headers;
using System.Text;

namespace PetStoreService.Web.Tests.Helper;

public static class Auth
{
    public static async Task<AuthenticationHeaderValue> Login(HttpClient client, string? email = null, string? password = null)
    {

        var loginRes = await client.PostAsync("/auth/login", new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            Email = email ?? MockIdentityManager.ValidRequest.Email,
            Password = password ?? MockIdentityManager.ValidRequest.Password
        }), Encoding.UTF8, "application/json"));

        if (loginRes.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception("Login failed");
        }

        var loginResBodyString = loginRes.Content.ReadAsStringAsync();
        var loginResBody = JsonConvert.DeserializeObject<LoginResponse>(loginResBodyString.Result);

        return new AuthenticationHeaderValue("Bearer", loginResBody!.JwtToken);
    }
}