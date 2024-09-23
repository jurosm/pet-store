using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PetStoreService.Application.Interfaces.IdentityManager;
using PetStoreService.Web.Tests.Mocks;

namespace PetStoreService.Web.Tests.Integration;

public class TestBase
{
    protected readonly WebApplicationFactory<Program> _factory;

    public TestBase()
    {
        var initialFactory = new WebApplicationFactory<Program>();
        _factory = initialFactory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IIdentityManager));

                services.AddSingleton<IIdentityManager, MockIdentityManager>();

                services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateActor = false,
                        SignatureValidator = (token, _) => new JsonWebToken(token),
                        RequireSignedTokens = false
                    };
                });
            });
        });
    }
}