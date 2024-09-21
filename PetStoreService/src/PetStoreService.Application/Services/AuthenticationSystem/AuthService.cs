using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.AspNetCore.Identity.Data;
using PetStoreService.Application.Models.Response.Auth;

namespace PetStoreService.Application.Services.AuthenticationSystem;

public class AuthService(AuthSettings authSettings)
{
    private readonly string _audience = authSettings.Auth0Audience;
    private readonly string _clientId = authSettings.Auth0ClientId;
    private readonly string _clientSecret = authSettings.Auth0ClientSecret;
    private readonly string _domain = authSettings.Auth0Domain;

    public async Task<LoginResponse> LoginUser(LoginRequest login)
    {
        AuthenticationApiClient auth = new(_domain);

        var res = await auth.GetTokenAsync(new ResourceOwnerTokenRequest()
        {
            Audience = _audience,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Username = login.Email,
            Password = login.Password,
            Scope = "openid profile email offline_access"
        });

        return new LoginResponse { JwtToken = res.AccessToken };
    }
}