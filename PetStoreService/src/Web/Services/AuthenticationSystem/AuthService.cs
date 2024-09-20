using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using PetStore.API.Models.Request.Auth;
using PetStore.API.Models.Response.Auth;
using System.Threading.Tasks;

namespace PetStore.API.Services.AuthenticationSystem
{
    public class AuthService(EnvironmentConfigurations envVariables)
    {
        private readonly string _audience = envVariables.Auth0Audience;
        private readonly string _clientId = envVariables.Auth0ClientId;
        private readonly string _clientSecret = envVariables.Auth0ClientSecret;
        private readonly string _domain = envVariables.Auth0Domain;

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
}