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
        private readonly EnvironmentConfigurations EnvVariables = new();

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

            services.AddExternalServices(EnvVariables.StripeSecret);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCors();

            services.AddPetStoreDBServices(EnvVariables.DBConnectionString);

            services.AddMyTOCServices();

            services.AddAuth0AuthenticationServices(EnvVariables.Auth0Domain, EnvVariables.Auth0Audience);

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