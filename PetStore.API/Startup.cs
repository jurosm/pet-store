using CodeSpaceBlog.API.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.API.Controllers;
using PetStore.API.Filters.GlobalFilters;
using PetStore.API.Services;
using PetStore.API.Services.AuthenticationSystem;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetStore.API.Services.ExternalServices;
using System;
using AspNetCoreRateLimit;

namespace PetStore.API
{

    public class Startup
    {
        private readonly EnvironmentConfigurations EnvVariables;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.EnvVariables = new EnvironmentConfigurations();
        }

        

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddRateLimitServices(Configuration.GetSection("IpRateLimiting"), Configuration.GetSection("IpRateLimitPolicies"));

            services.AddTransient<EnvironmentConfigurationService>();

            services.AddScoped<ExceptionActionFilter>();

            services.AddExternalServices(EnvVariables.StripeSecret);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Startup));

            services.AddCors();

            services.AddPetStoreDBServices(EnvVariables.DBConnectionString);

            services.AddMyTOCServices();

            services.AddAuth0AuthenticationServices(EnvVariables.Auth0Domain, EnvVariables.Auth0Audience);

            services.AddAuthorization();

            services.AddMyControllersServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIpRateLimiting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
          
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
