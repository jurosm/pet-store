using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetStoreService.Application.Interfaces.IdentityManager;
using PetStoreService.Application.Interfaces.IpInfoService;
using PetStoreService.Application.Interfaces.Payment;
using PetStoreService.Application.Models.Response.Toy;
using PetStoreService.Application.Services;
using PetStoreService.Application.Services.AuthenticationSystem;
using PetStoreService.Infrastructure.Auth0IdentityManager;
using PetStoreService.Infrastructure.IpInfoService;
using PetStoreService.Infrastructure.Payment;
using PetStoreService.Persistence;
using PetStoreService.Web.Controllers;
using PetStoreService.Web.Filters.GlobalFilters;
using System.Reflection;

namespace PetStoreService.Web;

public class Startup(IConfiguration configuration)
{
    private readonly EnvironmentConfigurations _envVariables = new();

    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        services.AddHttpLogging(options => { });
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddSingleton<AuthSettings>();
        services.AddSingleton<IIdentityManager, Auth0IdentityManager>();
        services.AddSingleton<EnvironmentConfigurations>();

        services.AddMemoryCache();

        services.AddScoped<ExceptionActionFilter>();

        services.AddScoped<IIpInfoService, IPInfoService>();
        services.AddScoped<IPaymentService, StripePaymentService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(ToyResponse)));

        services.AddCors();

        services.AddPetStoreDBServices();

        services.AddMyTOCServices();

        services.AddAuth0AuthenticationServices(_envVariables.AuthSettings.Auth0Domain, _envVariables.AuthSettings.Auth0Audience);

        services.AddAuthorization();

        services.AddMyControllersServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "documentation";
            });
        app.UseHttpLogging();

        app.UseCors("CorsPolicy");

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}