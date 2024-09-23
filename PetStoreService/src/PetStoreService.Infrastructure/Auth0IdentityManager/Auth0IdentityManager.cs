using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using PetStoreService.Application.Interfaces.IdentityManager;
using PetStoreService.Application.Services.AuthenticationSystem;

namespace PetStoreService.Infrastructure.Auth0IdentityManager;

public class Auth0IdentityManager(AuthSettings authSettings) : IIdentityManager
{
    private readonly string _audience = authSettings.Auth0Audience;
    private readonly string _clientId = authSettings.Auth0ClientId;
    private readonly string _clientSecret = authSettings.Auth0ClientSecret;
    private readonly string _domain = authSettings.Auth0Domain;

    public async Task<IdentityLoginResponse> Login(IdentityLoginRequest request)
    {
        AuthenticationApiClient auth = new(_domain);

        AccessTokenResponse res;
        try
        {
            res = await auth.GetTokenAsync(new ResourceOwnerTokenRequest()
            {
                Audience = _audience,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Username = request.Email,
                Password = request.Password,
                Scope = "openid profile email offline_access"
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return new IdentityLoginResponse(res.AccessToken);
    }
}