using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.AuthenticationSystem
{
    public static class AuthenticationServicesConfiguration
    {
        public static void AddAuth0AuthenticationServices(this IServiceCollection services, string domain, string audience)
        {
            services.AddScoped<AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"https://{domain}/";
                options.Audience = audience;
                options.SaveToken = true;
            });

        }
    }
}
