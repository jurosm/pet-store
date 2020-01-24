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

        public AuthService(IConfiguration configuration)
        {
            this.Audience = configuration["Auth0:Audience"];
            this.ClientId = configuration["Auth0:ClientId"];
            this.ClientSecret = configuration["Auth0:ClientSecret"];
            this.Domain = configuration["Auth0:Domain"];
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
