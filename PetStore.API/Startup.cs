using AspNetCoreRateLimit;
using CodeSpaceBlog.API.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStore.API.Controllers;
using PetStore.API.Filters.GlobalFilters;
using PetStore.API.Services;
using PetStore.API.Services.AuthenticationSystem;
using PetStore.API.Services.ExternalServices;
using System.Reflection;

namespace PetStore.API
{
    public class Startup(IConfiguration configuration)
    {
        private readonly EnvironmentConfigurations _envVariables = new();

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSingleton<EnvironmentConfigurations>();

            services.AddMemoryCache();

            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            services.AddRateLimitServices(Configuration.GetSection("IpRateLimiting"), Configuration.GetSection("IpRateLimitPolicies"));

            services.AddScoped<ExceptionActionFilter>();

            services.AddExternalServices(_envVariables.StripeSecret);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCors();

            services.AddPetStoreDBServices(_envVariables.DBConnectionString);

            services.AddMyTOCServices();

            services.AddAuth0AuthenticationServices(_envVariables.Auth0Domain, _envVariables.Auth0Audience);

            services.AddAuthorization();

            services.AddMyControllersServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            app.UseIpRateLimiting();

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