using Auth0.AuthenticationApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetStore.API.Models.Request.Auth;
using PetStore.API.Models.Response.Auth;
using Auth0.AuthenticationApi.Models;

namespace PetStore.API.Services.AuthenticationSystem
{
    public class AuthService
    {
        private string Audience, ClientId, ClientSecret, Domain;

        public AuthService()
        {
            this.Audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
            this.ClientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
            this.ClientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");
            this.Domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN");
        }

        public async Task<LoginResponse> LoginUser(LoginRequest login)
        {
            AuthenticationApiClient auth = new AuthenticationApiClient(this.Domain);

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
