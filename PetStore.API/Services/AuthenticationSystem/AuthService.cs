using Auth0.AuthenticationApi;
using System.Threading.Tasks;
using PetStore.API.Models.Request.Auth;
using PetStore.API.Models.Response.Auth;
using Auth0.AuthenticationApi.Models;

namespace PetStore.API.Services.AuthenticationSystem
{
    public class AuthService(EnvironmentConfigurations envVariables)
    {
        private readonly string Audience = envVariables.Auth0Audience;
        private readonly string ClientId = envVariables.Auth0ClientId;
        private readonly string ClientSecret = envVariables.Auth0ClientSecret;
        private readonly string Domain = envVariables.Auth0Domain;

        public async Task<LoginResponse> LoginUser(LoginRequest login)
        {
            AuthenticationApiClient auth = new(this.Domain);

            var res = await auth.GetTokenAsync(new ResourceOwnerTokenRequest()
            {
                Audience = this.Audience,
                ClientId = this.ClientId,
                ClientSecret = this.ClientSecret,
                Username = login.Email,
                Password = login.Password,
                Scope = "openid profile email offline_access"
            });

            return new LoginResponse { JwtToken = res.AccessToken };
        }
    }
}
